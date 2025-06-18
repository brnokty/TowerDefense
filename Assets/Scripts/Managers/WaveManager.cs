using System.Collections;
using UnityEngine;
using Zenject;

public class WaveManager : IInitializable
{
    private readonly EnemySpawner _enemySpawner;
    private readonly DiContainer _container;
    [HideInInspector] public int _currentWave = 0;
    private int _totalWaves = 3;

    private float _timeBetweenWaves = 5f;

    [Inject]
    public WaveManager(EnemySpawner enemySpawner, DiContainer container)
    {
        _enemySpawner = enemySpawner;
        _container = container;
    }

    public void Initialize()
    {
        Debug.Log("WaveManager Initialized");
        _container.InstantiateComponent<WaveController>(new GameObject("WaveController"));
    }

    // Dışarıdan çağırılır
    public void StartNextWave()
    {
        _currentWave++;
        Debug.Log($"🔥 Wave {_currentWave} başladı!");
        _enemySpawner.SpawnWave(_currentWave);
    }

    public bool AllWavesCompleted => _currentWave >= _totalWaves;
}