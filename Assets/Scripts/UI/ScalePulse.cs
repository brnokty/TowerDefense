using UnityEngine;
using DG.Tweening;

public class ScalePulse : MonoBehaviour
{
    [SerializeField] private float scaleUpSize = 1.5f;   // Büyüme boyutu
    [SerializeField] private float duration = 0.5f;      // Animasyon süresi

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        PlayPulseAnimation();
    }

    void PlayPulseAnimation()
    {
        // Önce büyüt, sonra küçült
        transform.DOScale(originalScale * scaleUpSize, duration)
            .SetLoops(-1, LoopType.Yoyo)  // Sonsuz döngü, büyü-küçük animasyonu
            .SetEase(Ease.InOutSine);     // Yumuşak geçiş
    }
}