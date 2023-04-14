using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class DestructibleBox : MonoBehaviour , IDamageable
{
    [SerializeField] private float _maxHealth = 100f;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = _maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
