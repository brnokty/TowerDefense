using System.Collections;
using UnityEngine;
using Zenject;

public class WaveController : MonoBehaviour
{
    private WaveManager _waveManager;
    [Inject] private TowerManager _towerManager;

    [Inject]
    public void Construct(WaveManager waveManager)
    {
        _waveManager = waveManager;
        StartCoroutine(SpawnWavesRoutine());
    }

    private IEnumerator SpawnWavesRoutine()
    {
        yield return new WaitForSeconds(1f);

        while (!_waveManager.AllWavesCompleted)
        {
            _towerManager.StartPlacementPhase();
            yield return new WaitForSeconds(5f);

            _waveManager.StartNextWave();
            yield return new WaitForSeconds(10f);
        }

        Debug.Log("🎉 Tüm düşman dalgaları tamamlandı!");
    }
}