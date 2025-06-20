using System.Collections;
using UnityEngine;
using Zenject;

public class WaveController : MonoBehaviour
{
    [Inject] private WaveManager _waveManager;
    [Inject] private TowerManager _towerManager;

    private void Start()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        yield return new WaitForSeconds(1f); 

        while (!_waveManager.AllWavesCompleted)
        {
            // 5 saniyelik kule yerleştirme süresi
            _towerManager.StartPlacementPhase();
            Debug.Log("Yerleştirme süresi başladı");

            yield return new WaitForSeconds(5f);

            // Wave başlasın
            _waveManager.StartNextWave();
            
            while (_waveManager.IsWaveInProgress)
            {
                yield return null;
            }

            yield return new WaitForSeconds(2f);
        }

        Debug.Log("Oyun tamamlandı!");
    }
}