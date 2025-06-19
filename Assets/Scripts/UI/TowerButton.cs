using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using Image = UnityEngine.UI.Image;

public class TowerButton : MonoBehaviour
{
    [Inject] private UIManager _uiManager;
    [HideInInspector] public int towerIndex;
    
    [SerializeField] private Image towerImage;
    [SerializeField] private TextMeshProUGUI towerCostText;
    [SerializeField] private TextMeshProUGUI toverNameText;
    private TowerData _towerData;

    
    public void Initialize(TowerData towerData, int index)
    {
        _towerData = towerData;
        towerIndex = index;
        towerImage.sprite = towerData.towerSprite;
        towerCostText.text = towerData.towerCost.ToString();
        toverNameText.text = towerData.towerName;
    }
    public void ButtonClick()
    {
        _uiManager.SetSelectedTower(towerIndex);
    }
}