using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformsController : MonoBehaviour
{
    [SerializeField] private Transform _pointA; // точка A
    [SerializeField] private Transform _pointB; // точка B
    [SerializeField, Range(0f, 3f)] private float _speed = 1.0f;

    private Vector2 _platformPosition;
    private Rigidbody2D _platformRigidbody;
    private Transform _target;

    private void Awake()
    {
        _platformRigidbody = GetComponent<Rigidbody2D>();
        _platformRigidbody.isKinematic = true;
        _target = _pointA;
    }


    private void FixedUpdate()
    {
        MovePlatform();
    }

    /// <summary>
    /// Метод движения платформы
    /// </summary>
    /// <param name="flag"></param>
    private void MovePlatform()
    {
        float distanceToTarget = Vector2.Distance(transform.position, _target.transform.position);

        if (distanceToTarget <= 0.05)
        {
            if (_target == _pointA) _target = _pointB;
            else if (_target == _pointB) _target = _pointA;
        }

        _platformPosition = Vector2.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * _speed);
        transform.position = _platformPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
