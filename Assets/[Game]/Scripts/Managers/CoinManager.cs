using UnityEngine;
using Zenject;

public class CoinManager : IInitializable
{
    [Inject] private UIManager _uiManager;
    private int _coins;

    public int Coins => _coins;

    public void Initialize()
    {
        _coins = 50; 
        _uiManager.SetCoin(_coins);
    }

    public bool CanAfford(int amount)
    {
        return _coins >= amount;
    }

    public void Spend(int amount)
    {
        _coins -= amount;
        _uiManager.SetCoin(_coins);
        Debug.Log($"Harcandı: -{amount}, Kalan: {_coins}");
    }

    public void Earn(int amount)
    {
        _coins += amount;
        _uiManager.SetCoin(_coins);
        Debug.Log($"Kazanıldı: +{amount}, Toplam: {_coins}");
    }
}