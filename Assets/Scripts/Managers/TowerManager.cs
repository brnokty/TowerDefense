using UnityEngine;
using Zenject;

public class TowerManager : ITickable
{
    private readonly Camera _mainCamera;
    private readonly GameObject _towerPrefab;
    private bool _canPlace = false;
    private float _placementTimer = 0f;
    private float _placementDuration = 5f;

    public TowerManager([Inject(Id = "TowerPrefab")] GameObject towerPrefab)
    {
        _towerPrefab = towerPrefab;
        _mainCamera = Camera.main;
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
            }
        }
    }

    private void TryPlaceTower()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 position = hit.point;
            position.y = 0f;

            ProjectContext.Instance.Container.InstantiatePrefab(_towerPrefab, position, Quaternion.identity, null);
            Debug.Log($"🏰 Kule yerleştirildi: {position}");
        }
    }
}