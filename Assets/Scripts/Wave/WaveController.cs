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
        yield return new WaitForSeconds(1f); // İlk geçiş için bekleme

        while (!_waveManager.AllWavesCompleted)
        {
            // 5 saniyelik kule yerleştirme süresi
            _towerManager.StartPlacementPhase();
            Debug.Log("🛠 Yerleştirme süresi başladı");

            yield return new WaitForSeconds(5f);

            // Wave başlasın
            _waveManager.StartNextWave();

            // Bekleme waveManager'da yapılır (hepsi ölene kadar)
            while (_waveManager.IsWaveInProgress)
            {
                yield return null;
            }

            yield return new WaitForSeconds(2f); // İsteğe bağlı wave arası bekleme
        }

        Debug.Log("🏁 Oyun tamamlandı!");
    }
}