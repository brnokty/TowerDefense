using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class AttackerBehavior : IEnemyBehavior
{
    private readonly Transform _enemyTransform;
    private readonly Transform _target;
    private readonly NavMeshAgent _agent;
    private readonly Enemy _enemy;
    private readonly DiContainer _container;

    private Tower _currentTarget;
    private float _attackCooldown = 1f;
    private float _timer;
    
    public Tower CurrentTargetTower => _currentTarget;


    public AttackerBehavior(Transform enemyTransform, Transform target, NavMeshAgent agent)
    {
        _enemyTransform = enemyTransform;
        _target = target;
        _agent = agent;

        _enemy = enemyTransform.GetComponent<Enemy>();
        _attackCooldown=_enemy._data.attackRate;
        _container = ProjectContext.Instance.Container;
    }
    

    public void Tick()
    {
        _timer -= Time.deltaTime;

        if (_currentTarget == null || !_currentTarget.gameObject.activeInHierarchy)
        {
            _currentTarget = null;
            _agent.isStopped = false;
            TryFindTower();
            MoveToBase();
        }
        else
        {
            // Kule hâlâ hayattaysa
            if (!_agent.isStopped)
            {
                _agent.isStopped = true;
                _agent.ResetPath();
            }

            AttackTower();
        }

        bool isMoving = !_agent.isStopped && _agent.velocity.magnitude > 0.1f;
        _enemy.SetWalkAnimation(isMoving);
    }


    private void TryFindTower()
    {
        Collider[] hits = Physics.OverlapSphere(_enemyTransform.position, _enemy._data.attackRange);
        foreach (var hit in hits)
        {
            Tower tower = hit.GetComponent<Tower>();
            if (tower != null)
            {
                _currentTarget = tower;
                _agent.isStopped = true;
                _agent.ResetPath();
                Debug.Log($"🔍 Kule bulundu: {_currentTarget.name}");
                break;
            }
        }
    }

    private void MoveToBase()
    {
        if (_target != null && _agent.destination != _target.position)
        {
            _agent.SetDestination(_target.position);
        }
    }

    private void AttackTower()
    {
        if (_currentTarget == null) return;

        // Kuleye yönel
        Vector3 direction = _currentTarget.transform.position - _enemyTransform.position;
        direction.y = 0f;

        _enemy.transform.LookAt(direction);
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _enemyTransform.rotation = Quaternion.Slerp(_enemyTransform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        if (_timer <= 0f)
        {
            _enemy._animator.SetTrigger("Attack");
            _timer = _attackCooldown; // tekrar başlat
        }
    }

       
    

    private void FireProjectile(Transform target)
    {
        var projectilePrefab = _enemy.ProjectilePrefab;

        var projectile = _container.InstantiatePrefabForComponent<Projectile>(
            projectilePrefab,
            _enemyTransform.position + Vector3.up * 1f,
            Quaternion.identity,
            null
        );

        projectile.SetProjectile(target, 10, 3f, 2);
    }

    public void FireProjectileFrom(Vector3 spawnPosition)
    {
        if (_currentTarget == null) return;

        var projectilePrefab = _enemy.ProjectilePrefab;

        var projectile = _container.InstantiatePrefabForComponent<Projectile>(
            projectilePrefab,
            spawnPosition,
            Quaternion.identity,
            null
        );

        projectile.SetProjectile(_currentTarget.transform, 10, 3f, 2);
    }
}