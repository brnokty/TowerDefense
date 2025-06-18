using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Enemy : MonoBehaviour, IDamageable
{
    [HideInInspector] public EnemyData _data;
    [SerializeField] private Animator _animator;
    [Inject] private CoinManager _coinManager;

    private int _maxHealth;
    private int _currentHealth;
    private bool _isDead;

    private float _originalSpeed;
    private float _currentSpeed;
    private float _slowTimer;

    [SerializeField] private HealthBar _healthBar;

    private IEnemyBehavior _behavior;
    private NavMeshAgent _agent;

    private WaveManager _waveManager;

    public void SetWaveManager(WaveManager manager)
    {
        _waveManager = manager;
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currentHealth = _maxHealth = _data.health;

        _originalSpeed = _data.speed;
        _currentSpeed = _originalSpeed;
        _agent.speed = _currentSpeed;

        var baseTarget = GameObject.FindWithTag("Base").transform;

        _behavior = _data.type == EnemyType.Runner
            ? new RunnerBehavior(transform, baseTarget, _agent)
            : new AttackerBehavior(transform, baseTarget, _agent);
    }

    private void Update()
    {
        if (_isDead) return;

        _behavior?.Tick();

        // slow etkisi bitti mi?
        if (_slowTimer > 0)
        {
            _slowTimer -= Time.deltaTime;
            if (_slowTimer <= 0)
            {
                _currentSpeed = _originalSpeed;
                _agent.speed = _currentSpeed;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        _healthBar.HealthBarUpdate(_maxHealth, _currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        _coinManager.Earn(_data.reward);

        _agent.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        transform.DORotate(new Vector3(0, 0, -90f), 0.5f);

        _waveManager?.NotifyEnemyKilled(); // 👈

        Destroy(gameObject, 1f);
    }


    public void ApplySlow(float slowPercent, float duration)
    {
        float actualSlow = slowPercent * _data.slowSensitivity;
        _currentSpeed = _originalSpeed * (1f - actualSlow);
        _agent.speed = _currentSpeed;
        _slowTimer = duration;

        Debug.Log($"{gameObject.name} yavaşlatıldı: %{actualSlow * 100}");
    }

    private void OnTriggerEnter(Collider other)
    {
        //base
        if (other.CompareTag("Base"))
        {
            // _waveManager?.NotifyBaseAttacked(); // 👈
            Die();
        }
    }
}