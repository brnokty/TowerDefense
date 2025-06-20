using UnityEngine;
using UnityEngine.AI;

public class RunnerBehavior : IEnemyBehavior
{
    private readonly Transform _target;
    private readonly NavMeshAgent _agent;
    private readonly Enemy _enemy;

    public RunnerBehavior(Transform enemyTransform, Transform target, NavMeshAgent agent)
    {
        _target = target;
        _agent = agent;
        _enemy = enemyTransform.GetComponent<Enemy>();
    }

    public void Tick()
    {
        if (_target != null && _agent.destination != _target.position)
        {
            _agent.SetDestination(_target.position);
        }
        
        bool isMoving = _agent.velocity.magnitude > 0.1f;
        _enemy.SetWalkAnimation(isMoving);
    }
}