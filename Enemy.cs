using UnityEngine;

[RequireComponent(typeof(Controller), typeof(EnemyAnimationController), typeof(Rigidbody2D))]
public class Enemy : Character, IDamageable
{

    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth;
    private EnemyAnimationController _enemyAnimationController;
    private Controller _enemyController;
    private Rigidbody2D _enemyRigidbody;
    private Collider2D _enemyCollider;
    public bool IsDead { get; private set; }

    private void Awake()
    {
        IsDead = false;
        _enemyAnimationController = GetComponent<EnemyAnimationController>();
        _enemyController = GetComponent<Controller>();
        _enemyRigidbody = GetComponent<Rigidbody2D>();
        _enemyCollider = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }
    private void Update()
    {
        if (IsDead)
        {
            _enemyRigidbody.isKinematic = true;
            _enemyCollider.enabled = false;
            _enemyController.input = null;
        }
    }

    public override void Death()
    {
        IsDead = true;
        Destroy(gameObject, 1f);
    }

    public void TakeDamage(float damageAmount)
    {
        _enemyAnimationController.IsHurting();
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0)
        {
            Death();
        }
    }
}
