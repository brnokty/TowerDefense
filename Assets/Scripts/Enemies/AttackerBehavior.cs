using UnityEngine;
using UnityEngine.AI;

public class AttackerBehavior : IEnemyBehavior
{
    private readonly Transform _enemyTransform;
    private readonly Transform _target;
    private readonly NavMeshAgent _agent;

    private Tower _currentTarget;
    private float _attackCooldown = 1f;
    private float _timer;

    public AttackerBehavior(Transform enemyTransform, Transform target, NavMeshAgent agent)
    {
        _enemyTransform = enemyTransform;
        _target = target;
        _agent = agent;
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
            _currentTarget.TakeDamage(10);
            _timer = _attackCooldown;
        }
    }
}