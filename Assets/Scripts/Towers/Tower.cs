using UnityEngine;
using Zenject;

public class Tower : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private HealthBar _healthBar;

    [HideInInspector] public TowerData _data;
    [Inject(Id = "ProjectilePrefab")] private GameObject _projectilePrefab;

    private float _fireCooldown;
    private float _healCooldown;
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = 100;
    }

    private void Update()
    {
        if (_data.towerType == TowerType.Shooter)
            HandleShooting();
        else if (_data.towerType == TowerType.Support)
            HandleSupport();
    }

    private void HandleShooting()
    {
        _fireCooldown -= Time.deltaTime;

        Collider[] hits = Physics.OverlapSphere(transform.position, _data.range);

        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy == null) continue;

            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && _fireCooldown <= 0f)
        {
            Shoot(closestEnemy.transform);
            _fireCooldown = 1f / _data.fireRate;
        }
    }

    private void Shoot(Transform target)
    {
        var projectile = ProjectContext.Instance.Container.InstantiatePrefabForComponent<Projectile>(
            _projectilePrefab,
            firePoint.position,
            Quaternion.identity,
            null
        );
        
        int projectileEffectIndex = 0;
        if (_data.towerIndex == 1)
            projectileEffectIndex = 1; // İkinci kule için farklı efekt

        projectile.SetProjectile(target, _data.damage, 10f, projectileEffectIndex);
    }

    private void HandleSupport()
    {
        _healCooldown -= Time.deltaTime;
        if (_healCooldown > 0f) return;

        Collider[] hits = Physics.OverlapSphere(transform.position, _data.range);

        foreach (var hit in hits)
        {
            var tower = hit.GetComponent<Tower>();
            if (tower != null && tower != this)
            {
                tower.ReceiveHeal((int)_data.healAmount);
            }

            var enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ApplySlow(_data.slowAmount, _data.slowDuration);
            }
        }

        _healCooldown = _data.healInterval;
    }

    public void ReceiveHeal(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Min(_currentHealth, 100);
        _healthBar.HealthBarUpdate(100, _currentHealth);
        Debug.Log($"{_data.towerName} can aldı: {_currentHealth}");
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        _healthBar.HealthBarUpdate(100, _currentHealth);
        Debug.Log($"🏰 Kule hasar aldı: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("💥 Kule yok edildi!");
        }
    }
}