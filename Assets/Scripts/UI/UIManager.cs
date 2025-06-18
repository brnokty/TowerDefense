using UnityEngine;
using Zenject;

public class UIManager : IInitializable, ITickable
{
    [Inject] private InGamePanel _inGamePanel;
    [Inject] private TowerManager _towerManager;

    public void Initialize()
    {
        _inGamePanel.UpdateWave(1);
    }

    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _towerManager.SelectTower(0); // Shooter
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _towerManager.SelectTower(1); // Support
        }
    }

    public void SetCoin(int coin)
    {
        _inGamePanel.UpdateCoin(coin);
    }

    public void SetWave(int wave)
    {
        _inGamePanel.UpdateWave(wave);
    }
}