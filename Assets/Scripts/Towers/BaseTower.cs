using UnityEngine;
using Zenject;

public class BaseTower : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private ParticleSystem damageEffect;

    private int _currentHealth;
    private bool _isDestroyed;

    [Inject] private UIManager _uiManager;

    private void Start()
    {
        _currentHealth = maxHealth;
        _healthBar.HealthBarUpdate(maxHealth, _currentHealth);
    }

    public void TakeDamage(int amount)
    {
        if (_isDestroyed) return;

        damageEffect.Play();
        _currentHealth -= amount;
        _healthBar.HealthBarUpdate(maxHealth, _currentHealth);

        if (_currentHealth <= 0)
        {
            _isDestroyed = true;
            _uiManager.ShowLosePanel();
        }
    }
}