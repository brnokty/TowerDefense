using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class InGamePanel : Panel
{
    [Inject] private TowerManager _towerManager;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private GameObject towerButtonPrefab;
    [SerializeField] private Transform _context;
    [SerializeField] private Image selectedTowerImage;
    [Inject] private TowerData[] _availableTowers;
    [Inject] private DiContainer _container;

    protected override void Start()
    {
        base.Start();
        UpdateTowerButtons(_availableTowers);
        SetSelectedTower(0);
    }

    public void UpdateCoin(int amount)
    {
        coinText.text = amount.ToString();
    }

    public void UpdateWave(int wave)
    {
        waveText.text = wave.ToString();
    }

    //Instantiates tower buttons based on available towers
    public void UpdateTowerButtons(TowerData[] towers)
    {
        foreach (Transform child in _context)
        {
            Destroy(child.gameObject);
        }

        if (towers == null || towers.Length == 0)
        {
            Debug.LogWarning("No towers available to display.");
            return;
        }

        int index = 0;
        foreach (var tower in towers)
        {
            var towerButton = _container.InstantiatePrefabForComponent<TowerButton>(towerButtonPrefab, _context);

            towerButton.Initialize(tower, index);
            index++;
        }
    }

    public void SetSelectedTower(int towerIndex)
    {
        _towerManager.SelectTower(towerIndex);

        selectedTowerImage.sprite = _availableTowers[towerIndex].towerSprite;
    }
}