using UnityEngine;
using Zenject;
using System.Collections;

public class WaveManager : IInitializable
{
    private readonly EnemySpawner _spawner;
    private readonly WaveData[] _waves;
    private readonly UIManager _uiManager;
    [Inject] private TowerManager _towerManager;

    private int _currentWave = -1;
    private int _enemiesRemaining = 0;
    private bool _waveInProgress = false;

    public bool IsWaveInProgress => _waveInProgress;


    public int CurrentWave => _currentWave + 1;
    public bool AllWavesCompleted => _currentWave + 1 >= _waves.Length;

    public WaveManager(
        EnemySpawner spawner,
        WaveData[] waves,
        UIManager uiManager
    )
    {
        _spawner = spawner;
        _waves = waves;
        _uiManager = uiManager;
    }

    public void Initialize()
    {
        Debug.Log("📦 WaveManager initialized");
        //StartNextWave();
        _towerManager.StartPlacementPhase();
    }

    public void StartNextWave()
    {
        _currentWave++;

        if (_currentWave >= _waves.Length)
        {
            Debug.Log("✅ Tüm dalgalar bitti!");
            _uiManager.ShowWinPanel();
            return;
        }

        var wave = _waves[_currentWave];
        _enemiesRemaining = wave.TotalEnemies;
        _waveInProgress = true;

        _uiManager.SetWave(CurrentWave);

        _spawner.SpawnWave(wave, this);
    }

    public void NotifyEnemyKilled()
    {
        _enemiesRemaining--;

        if (_enemiesRemaining <= 0 && _waveInProgress)
        {
            _waveInProgress = false;
            Debug.Log($"🌪 Wave {CurrentWave} tamamlandı!");


            _spawner.StartCoroutine(WaitAndStartNextWave());
        }
    }

    private IEnumerator WaitAndStartNextWave()
    {
        yield return new WaitForSeconds(2f);
        // StartNextWave();
        _towerManager.StartPlacementPhase();
    }
}