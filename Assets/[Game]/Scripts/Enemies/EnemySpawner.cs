using System.Collections;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform targetPoint;

    [Inject] private DiContainer _container;

    public void SpawnWave(WaveData waveData, WaveManager manager)
    {
        StartCoroutine(SpawnWaveCoroutine(waveData, manager));
    }

    private IEnumerator SpawnWaveCoroutine(WaveData waveData, WaveManager manager)
    {
        for (int i = 0; i < waveData.enemyTypes.Length; i++)
        {
            EnemyData data = waveData.enemyTypes[i];
            int count = waveData.enemyCounts[i];

            for (int j = 0; j < count; j++)
            {
                var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                var enemy = _container.InstantiatePrefabForComponent<Enemy>(
                    data.enemyPrefab,
                    spawnPoint.position,
                    Quaternion.identity,
                    null
                );
                enemy._data = data;
                enemy.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

                _container.Inject(enemy);

                enemy.SetWaveManager(manager);

                yield return new WaitForSeconds(waveData.spawnDelay);
            }
        }
    }
}