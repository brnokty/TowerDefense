using UnityEngine;

public class AttackerBehavior : IEnemyBehavior
{
    private readonly Transform _enemyTransform;
    private readonly float _speed;
    private Tower _currentTarget;
    private float _attackCooldown = 1f;
    private float _timer;

    public AttackerBehavior(Transform enemyTransform, float speed)
    {
        _enemyTransform = enemyTransform;
        _speed = speed;
    }

    public void Tick()
    {
        _timer -= Time.deltaTime;

        if (_currentTarget == null)
        {
            TryFindTower();
            MoveForward();
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

    private void MoveForward()
    {
        _enemyTransform.position += _enemyTransform.forward * _speed * Time.deltaTime;
    }

    private void AttackTower()
    {
        if (_currentTarget == null) return;

        if (_timer <= 0f)
        {
            _currentTarget.TakeDamage(10);
            _timer = _attackCooldown;
        }
    }
}