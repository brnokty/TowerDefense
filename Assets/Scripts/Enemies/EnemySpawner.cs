using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform targetPoint;

    [Inject] private EnemyData _enemyData;
    [Inject(Id = "EnemyPrefab")] private GameObject _enemyPrefab;

    public void SpawnWave(int waveNumber)
    {
        int enemyCount = 1;//waveNumber * 3;

        for (int i = 0; i < enemyCount; i++)
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            var enemy = ProjectContext.Instance.Container.InstantiatePrefabForComponent<Enemy>(
                _enemyPrefab,
                spawnPoint.position,
                Quaternion.identity,
                null
            );

            // enemy.SetTarget(targetPoint);
            
        }
    }
}