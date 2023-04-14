using UnityEngine;

[RequireComponent(typeof(Controller), typeof(CollisionDataRetriever), typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;


    private Controller _controller;
    private Rigidbody2D _body;
    private CollisionDataRetriever _ground;

    private Vector2 _direction, _velocity, _desiredVelocity;
    private float _maxSpeedChange, _acceleration;
    private bool _onGround;
    public bool IsRunning { get; private set; }

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<CollisionDataRetriever>();
        _controller = GetComponent<Controller>();
    }

    void Update()
    {
        if (_controller.input != null) _direction.x = _controller.input.RetrieveMoveInput(this.gameObject);
        else _direction.x = 0;

        IsRunning = _direction.x != 0f;

        // ��������� �������� � ������� ���������� ������ ���� ������ ����� �������, ����� ��� ������� �� ���� ������ 0

        _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _ground.Friction, 0f);
    }

    private void FixedUpdate()
    {
        _onGround = _ground.OnGround;
        _velocity = _body.velocity;

        // � ����������� �� �������������� ��������� ������� � ����������� ��� ��������������� ���������

        _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
        _maxSpeedChange = _acceleration * Time.deltaTime;

        // ��������� �������, � ������� ����� ��������� ��������, � ������ ���������

        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

        _body.velocity = _controller.input != null ? _velocity : Vector2.zero;
    }
}

