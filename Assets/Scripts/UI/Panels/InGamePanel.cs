using UnityEngine;
using TMPro;

public class InGamePanel : Panel
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI waveText;

    public void UpdateCoin(int amount)
    {
        coinText.text = amount.ToString();
    }

    public void UpdateWave(int wave)
    {
        waveText.text =  wave.ToString();
    }
}