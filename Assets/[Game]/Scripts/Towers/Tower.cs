using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Tower : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform rangeTransform;
    [SerializeField] private HealthBar _healthBar;

    [HideInInspector] public TowerData _data;
    [Inject(Id = "ProjectilePrefab")] private GameObject _projectilePrefab;

    private float _fireCooldown;
    private float _healCooldown;
    private int _currentHealth;
    [SerializeField] private List<ParticleSystem> _effects;

    private void Start()
    {
        _currentHealth = 100;
        rangeTransform.localScale = Vector3.one * 2 * _data.towerRange;
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

        Collider[] hits = Physics.OverlapSphere(transform.position, _data.towerRange);

        List<Enemy> targetingThisTower = new List<Enemy>();
        List<Enemy> otherEnemies = new List<Enemy>();

        foreach (var hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy == null || enemy.IsDead) continue;

            if (enemy.CurrentTargetTower == this)
            {
                targetingThisTower.Add(enemy);
            }
            else
            {
                otherEnemies.Add(enemy);
            }
        }

        List<Enemy> prioritizedList = targetingThisTower.Count > 0 ? targetingThisTower : otherEnemies;

        Enemy selectedEnemy = null;
        float lowestHealth = Mathf.Infinity;
        float closestDistance = Mathf.Infinity;

        foreach (var enemy in prioritizedList)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            int health = enemy.CurrentHealth;

            if (health < lowestHealth ||
                (health == lowestHealth && dist < closestDistance))
            {
                selectedEnemy = enemy;
                lowestHealth = health;
                closestDistance = dist;
            }
        }

        if (selectedEnemy != null && _fireCooldown <= 0f)
        {
            Shoot(selectedEnemy.transform);
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

        PlayEffects();

        Collider[] hits = Physics.OverlapSphere(transform.position, _data.towerRange);

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

        _healCooldown = _data.supportInterval;
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
        Debug.Log($"Kule hasar aldı: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Kule yok edildi");
        }
    }

    private void PlayEffects()
    {
        foreach (var effect in _effects)
        {
            if (effect != null)
            {
                effect.Play();
            }
        }
    }
}