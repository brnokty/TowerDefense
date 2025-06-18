using UnityEngine;
using UnityEngine.AI;

public class RunnerBehavior : IEnemyBehavior
{
    private readonly Transform _enemyTransform;
    private readonly Transform _target;
    private readonly float _speed;
    private NavMeshAgent agent;

    public RunnerBehavior(Transform enemyTransform, Transform target, float speed)
    {
        _enemyTransform = enemyTransform;
        _target = target;
        _speed = speed;
        agent= enemyTransform.GetComponent<NavMeshAgent>();
    }

    public void Tick()
    {
        if (_target == null) return;
        
        
        if (_target != null && agent.destination != _target.position)
        {
            agent.SetDestination(_target.position);
        }
    }
}