using UnityEngine;
using UnityEngine.AI;

public class RunnerBehavior : IEnemyBehavior
{
    private readonly Transform _target;
    private readonly NavMeshAgent _agent;

    public RunnerBehavior(Transform enemyTransform, Transform target, NavMeshAgent agent)
    {
        _target = target;
        _agent = agent;
    }

    public void Tick()
    {
        if (_target != null && _agent.destination != _target.position)
        {
            _agent.SetDestination(_target.position);
        }
    }
}