using UnityEngine;
using Zenject;

public class EnemyFactory
{
    private readonly DiContainer _container;
    private readonly EnemyData _enemyData;
    private readonly GameObject _enemyPrefab;

    public EnemyFactory(DiContainer container, EnemyData data, GameObject prefab)
    {
        _container = container;
        _enemyData = data;
        _enemyPrefab = prefab;
    }

    public Enemy Create(Vector3 position, Transform target)
    {
        var enemy = _container.InstantiatePrefabForComponent<Enemy>(
            _enemyPrefab,
            position,
            Quaternion.identity,
            null
        );

        // enemy.SetTarget(target);
        return enemy;
    }
}