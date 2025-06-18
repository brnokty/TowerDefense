using UnityEngine;
using Zenject;

public class UIManager : IInitializable
{
    [Inject] private InGamePanel _inGamePanel;


    public void Initialize()
    {
        _inGamePanel.UpdateWave(1);
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