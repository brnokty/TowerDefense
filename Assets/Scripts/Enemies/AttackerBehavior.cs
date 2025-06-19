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

    public AttackerBehavior(Transform enemyTransform, Transform target, NavMeshAgent agent)
    {
        _enemyTransform = enemyTransform;
        _target = target;
        _agent = agent;

        _enemy = enemyTransform.GetComponent<Enemy>();
        _container = ProjectContext.Instance.Container; // Zenject erişimi
    }

    public void Tick()
    {
        _timer -= Time.deltaTime;

        if (_currentTarget == null)
        {
            TryFindTower();
            MoveToBase();
        }
        else
        {
            AttackTower();
        }

        bool isMoving = _agent.velocity.magnitude > 0.1f;
        _enemy.SetWalkAnimation(isMoving);
    }

    private void TryFindTower()
    {
        Collider[] hits = Physics.OverlapSphere(_enemyTransform.position, 2f);
        foreach (var hit in hits)
        {
            Tower tower = hit.GetComponent<Tower>();
            if (tower != null)
            {
                _currentTarget = tower;
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
        if (_timer <= 0f && _currentTarget != null)
        {
            FireProjectile(_currentTarget.transform);
            _timer = _attackCooldown;
        }
    }

    private void FireProjectile(Transform target)
    {
        var projectilePrefab = _enemy.ProjectilePrefab; // Enemy'den prefab referansı alınır

        var projectile = _container.InstantiatePrefabForComponent<Projectile>(
            projectilePrefab,
            _enemyTransform.position + Vector3.up * 1f, // elinden veya ortadan çıksın
            Quaternion.identity,
            null
        );

        projectile.SetProjectile(target, 10, 3f);
    }
}
