using UnityEngine;

public class CollisionDataRetriever : MonoBehaviour
{

    public bool OnGround { get; private set; }
    public bool OnWall { get; private set; }
    public float Friction { get; private set; }
    public Vector2 ContactNormal { get; private set; }

    private PhysicsMaterial2D _material;


    private void OnCollisionExit2D(Collision2D collision)
    {
        OnGround = false;
        Friction = 0;
        OnWall = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }

    /// <summary>
    /// Оценка всех точек соприкасновения
    /// </summary>
    /// <param name="collision"></param>
    public void EvaluateCollision(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            //Определяем наличие контакта с горизонтальной поверностью
            ContactNormal = collision.GetContact(i).normal;
            OnGround |= ContactNormal.y >= 0.01f;

            // Определяем наличие контакта с вертикальной поверхностью, при условии определенного тега
            if (collision.rigidbody.CompareTag("Wall"))
            {
                OnWall = Mathf.Abs(ContactNormal.x) >= 0.01f;
            }
        }
    }

    /// <summary>
    /// Оценка воздествующей силы трения
    /// </summary>
    /// <param name="collision"></param>
    private void RetrieveFriction(Collision2D collision)
    {
        _material = collision.rigidbody.sharedMaterial;
        Friction = _material != null ? _material.friction : 0;
    }
}
