using UnityEngine;
using Zenject;

public class Tower : MonoBehaviour, IDamageable
{
    [SerializeField] private float range = 5f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int damage = 10;
    [SerializeField] private HealthBar _healthBar;

    private float _fireCooldown;
    private int _currentHealth;

    [Inject(Id = "ProjectilePrefab")] private GameObject _projectilePrefab;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    private void Update()
    {
        _fireCooldown -= Time.deltaTime;

        Collider[] hits = Physics.OverlapSphere(transform.position, range);

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
            _fireCooldown = 1f / fireRate;
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

        projectile.SetProjectile(target, damage, 10f);
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        _healthBar.HealthBarUpdate(maxHealth, _currentHealth);
        Debug.Log($"🏰 Kule hasar aldı: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("💥 Kule yok edildi!");
        }
    }
}