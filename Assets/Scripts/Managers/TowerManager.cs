using UnityEngine;
using Zenject;

public class TowerManager : ITickable
{
    private readonly Camera _mainCamera;
    private readonly TowerData[] _towerDatas;
    private readonly CoinManager _coinManager;
    private readonly DiContainer _container;
    [Inject] private WaveManager _waveManager;

    private TowerData _selectedTowerData;
    private bool _canPlace = false;
    private float _placementTimer = 0f;
    private float _placementDuration = 5f;

    public TowerManager(
        TowerData[] towerDatas,
        CoinManager coinManager,
        DiContainer container
    )
    {
        _towerDatas = towerDatas;
        _coinManager = coinManager;
        _container = container;
        _mainCamera = Camera.main;

        _selectedTowerData = _towerDatas[0]; // default shooter
    }

    public void StartPlacementPhase()
    {
        _placementTimer = _placementDuration;
        _canPlace = true;
        Debug.Log("🛠 Kule yerleştirme başladı (5 saniye)");
    }

    public void Tick()
    {
        if (_canPlace)
        {
            _placementTimer -= Time.deltaTime;

            if (Input.GetMouseButtonDown(0))
            {
                TryPlaceTower();
            }

            if (_placementTimer <= 0f)
            {
                _canPlace = false;
                Debug.Log("🔔 Kule yerleştirme süresi bitti");
                _waveManager.StartNextWave();
            }
        }
    }

    private void TryPlaceTower()
    {
        if (!_coinManager.CanAfford(_selectedTowerData.towerCost))
        {
            Debug.Log("🚫 Coin yetersiz.");
            return;
        }

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 position = hit.point;
            position.y = 0f;

            var tower = _container.InstantiatePrefabForComponent<Tower>(
                _selectedTowerData.towerPrefab,
                position,
                Quaternion.identity,
                null
            );
            tower._data = _selectedTowerData;

            _container.Inject(tower); // TowerData injection

            _coinManager.Spend(_selectedTowerData.towerCost);
            Debug.Log($"🏰 {_selectedTowerData.towerName} yerleştirildi: {position}");
        }
    }

    public void SelectTower(int index)
    {
        if (index >= 0 && index < _towerDatas.Length)
        {
            _selectedTowerData = _towerDatas[index];
            Debug.Log($"✅ Seçilen kule: {_selectedTowerData.towerName}");
        }
    }
}