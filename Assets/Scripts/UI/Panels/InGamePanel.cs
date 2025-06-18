using UnityEngine;
using TMPro;
using Zenject;

public class InGamePanel : Panel
{
    [Inject] private TowerManager _towerManager;
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
    
    public void Button1_OnClick()
    {
        _towerManager.SelectTower(0); // Shooter
    }
    public void Button2_OnClick()
    {
        _towerManager.SelectTower(1); // Support
    }
    public void Button3_OnClick()
    {
        _towerManager.SelectTower(2); // Faster
    }
    public void Button4_OnClick()
    {
        _towerManager.SelectTower(3); // Slower
    }
    
}