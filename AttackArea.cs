using UnityEngine;

[RequireComponent(typeof (Collider2D))]
public class AttackArea : MonoBehaviour
{

    [SerializeField] private float _damage = 0f;
    [SerializeField] private string _tag = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_tag != null)
        {
            if (collision.CompareTag(_tag))
            {
                if (collision.TryGetComponent(out IDamageable hit))
                {
                    hit.TakeDamage(_damage);
                }
            }
        }
    }
}
