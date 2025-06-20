using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
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
    [Inject] private UIManager _uiManager;

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
        _uiManager.SetTowerUIEnabled(true);
        Debug.Log("Kule yerleştirme başladı (5 saniye)");
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
                _uiManager.SetTowerUIEnabled(false);
                Debug.Log("Kule yerleştirme süresi bitti");
                _waveManager.StartNextWave();
            }
        }
    }

    private void TryPlaceTower()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // Para kontrolü
        if (!_coinManager.CanAfford(_selectedTowerData.towerCost))
        {
            Debug.Log("🚫 Coin yetersiz.");
            return;
        }

        // Raycast
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (!hit.collider.CompareTag("Platform"))
            {
                return;
            }


            bool isWalkable = IsPositionOnNavMesh(hit.point);

            if (isWalkable)
            {
                return;
            }

            Vector3 position = hit.point;
            position.y = 0f;

            // Kule instantiate et
            var tower = _container.InstantiatePrefabForComponent<Tower>(
                _selectedTowerData.towerPrefab,
                position,
                Quaternion.identity,
                null
            );
            tower._data = _selectedTowerData;

            _container.Inject(tower);

            // Para harcama
            _coinManager.Spend(_selectedTowerData.towerCost);
        }
    }

    public void SelectTower(int index)
    {
        if (index >= 0 && index < _towerDatas.Length)
        {
            _selectedTowerData = _towerDatas[index];
            Debug.Log($"seçilen kule: {_selectedTowerData.towerName}");
        }
    }

    private bool IsPositionOnNavMesh(Vector3 position)
    {
        NavMeshHit navHit;
        return NavMesh.SamplePosition(position, out navHit, 1f, NavMesh.AllAreas);
    }
}