using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Enemy : MonoBehaviour, IDamageable
{
    [Inject] private EnemyData _data;
    private int _maxHealth;
    private int _currentHealth;
    private bool _isDead;
    [SerializeField] private HealthBar _healthBar;

    private IEnemyBehavior _behavior;

    private void Start()
    {
        _currentHealth = _data.health;
        _maxHealth = _data.health;


        if (_data.type == EnemyType.Runner)
        {
            _behavior = new RunnerBehavior(transform, GameObject.FindWithTag("Base").transform, _data.speed);
        }
        else
        {
            _behavior = new AttackerBehavior(transform, _data.speed);
        }
    }

    private void Update()
    {
        if (_isDead) return;
        //_behavior?.Tick();
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        _healthBar.HealthBarUpdate(_maxHealth, _currentHealth);
        if (_currentHealth <= 0)
        {
            transform.DORotate(new Vector3(0, 0, -90f), 0.5f);

            _isDead = true;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<TestNav>().enabled = false;
            Destroy(gameObject, 1f);
        }
    }
}