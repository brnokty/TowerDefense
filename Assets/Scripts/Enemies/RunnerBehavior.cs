using UnityEngine;

public class RunnerBehavior : IEnemyBehavior
{
    private readonly Transform _enemyTransform;
    private readonly Transform _target;
    private readonly float _speed;

    public RunnerBehavior(Transform enemyTransform, Transform target, float speed)
    {
        _enemyTransform = enemyTransform;
        _target = target;
        _speed = speed;
    }

    public void Tick()
    {
        if (_target == null) return;

        Vector3 dir = (_target.position - _enemyTransform.position).normalized;
        _enemyTransform.position += dir * _speed * Time.deltaTime;
    }
}