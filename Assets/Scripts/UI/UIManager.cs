using UnityEngine;
using Zenject;

public class UIManager : IInitializable
{
    [Inject] private InGamePanel _inGamePanel;
    [Inject] private WinPanel _winPanel;
    [Inject] private LosePanel _losePanel;


    public void Initialize()
    {
        ShowInGamePanel();
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

    public void SetSelectedTower(int towerIndex)
    {
        _inGamePanel.SetSelectedTower(towerIndex);
    }

    public void ShowWinPanel()
    {
        _winPanel.Appear();
        //Disappear otherpanels
        _inGamePanel.Disappear();
        _losePanel.Disappear();
    }

    public void ShowLosePanel()
    {
        _losePanel.Appear();
        //Disappear other panels
        _inGamePanel.Disappear();
        _winPanel.Disappear();
    }

    public void ShowInGamePanel()
    {
        _inGamePanel.Appear();
        //Disappear other panels
        _winPanel.Disappear();
        _losePanel.Disappear();
    }
}