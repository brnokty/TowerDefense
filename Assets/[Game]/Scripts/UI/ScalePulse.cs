using UnityEngine;
using DG.Tweening;

public class ScalePulse : MonoBehaviour
{
    [SerializeField] private float scaleUpSize = 1.5f;   
    [SerializeField] private float duration = 0.5f;      

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        PlayPulseAnimation();
    }

    void PlayPulseAnimation()
    {
        
        transform.DOScale(originalScale * scaleUpSize, duration)
            .SetLoops(-1, LoopType.Yoyo) 
            .SetEase(Ease.InOutSine);     
    }
}