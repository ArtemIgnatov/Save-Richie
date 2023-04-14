using UnityEngine;
[RequireComponent(typeof(Collider2D))]

public class LifeBonus : MonoBehaviour
{
    [SerializeField, Range(0f,100f)] private float _healAmount = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.TryGetComponent(out Player player);
            player.Heal(_healAmount);
            Destroy(this.gameObject, 1f);
        }
    }
}
