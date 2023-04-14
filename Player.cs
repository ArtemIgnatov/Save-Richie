using UnityEngine;

[RequireComponent(typeof(Controller), typeof(PlayerAnimationController))]
public class Player : Character, IDamageable
{
    [SerializeField] private Healthbar _healthbar;
    [SerializeField] private float _maxHealth = 100f;

    private float _currentHealth;
    public bool IsDead { get; set; }

    private PlayerAnimationController _playerAnimationController;
    private Controller _playerController;
    private Rigidbody2D _playerRigidbody;
    private Collider2D _playerCollider;



    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<Collider2D>();
        _playerController = GetComponent<Controller>();
        _playerAnimationController = GetComponent<PlayerAnimationController>();
        IsDead = false;
    }

    void Start()
    {
        _currentHealth = _maxHealth;
        _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
    }

    /// <summary>
    /// Смерть
    /// </summary>
    public override void Death()
    {
        IsDead = true;
        DisableController();
        _playerRigidbody.isKinematic = true;
        _playerCollider.enabled = false;
    }

    /// <summary>
    /// Получение урона
    /// </summary>
    /// <param name="damageAmount"></param>
    public void TakeDamage(float damageAmount)
    {
        _playerAnimationController.IsHurting();
        _currentHealth -= damageAmount;
        _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);

        if (_currentHealth <= 0) Death();
    }

    public void Heal(float healAmount)
    {
        if (_currentHealth > 0)
            _currentHealth = _currentHealth + healAmount > _maxHealth ? _maxHealth : _currentHealth + healAmount;

        _healthbar.UpdateHealthBar(_maxHealth, _currentHealth);
    }

    public void DisableController()
    {
        _playerController.input = null;
    }
}
