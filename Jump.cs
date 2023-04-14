using UnityEngine;

[RequireComponent(typeof(Controller), typeof(CollisionDataRetriever), typeof(Rigidbody2D))]
public class Jump : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float _jumpHeight = 3f;
    [SerializeField, Range(0f, 5)] private int _maxAirJumps = 0; // Сколько прыжков в воздухе может сделать персонаж
    [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 3f; // Скорость движения вниз
    [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 1.7f; // Скорость движения вверх
    [SerializeField, Range(0f, 0.5f)] private float _coyoteTime = 0.2f;
    [SerializeField, Range(0.1f, 0.5f)] private float _jumpBufferTime = 0.3f;

    private Controller _controller;
    private Rigidbody2D _body;
    private CollisionDataRetriever _ground;
    private Vector2 _velocity;

    private int _jumpPhase; // Сколько раз персонаж прыгнул
    private float _defaultGravityScale, _jumpSpeed, _coyoteCounter, _jumpBufferCounter; 
    private bool _desiredJump, _onGround, _onWall; 

    public bool IsJumping { get; private set; }
    public bool IsFalling { get; private set; }


    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<CollisionDataRetriever>();
        _controller = GetComponent<Controller>();

        _defaultGravityScale = 1f;

        IsFalling = false;
        IsJumping = false;
    }

    void Update()
    {
        if (_controller.input != null)
            _desiredJump |= _controller.input.RetrieveJumpInput(this.gameObject);
    }

    private void FixedUpdate()
    {
        _onGround = _ground.OnGround;
        _onWall = _ground.OnWall;
        _velocity = _body.velocity;

        IsFalling = _body.velocity.y < 1f && !_onGround;

        if (_onGround || _onWall)
        {
            _jumpPhase = 0;
            _coyoteCounter = _coyoteTime;
            IsJumping = false;
        }
        else
        {
            _coyoteCounter -= Time.deltaTime;
        }

        if (_desiredJump)
        {
            _desiredJump = false;
            _jumpBufferCounter = _jumpBufferTime;
        }
        else if (!_desiredJump && _jumpBufferCounter > 0)
        {
            _jumpBufferCounter -= Time.deltaTime;
        }

        if (_jumpBufferCounter > 0) JumpAction();


        if (_controller.input != null)
        {
            // Определяем гравитацию во время прыжка в зависимости от того
            // движется тело вверх или вниз или находится неподвижным

            if (_controller.input.RetrieveJumpHoldInput(this.gameObject) && _body.velocity.y > 0)
                _body.gravityScale = _upwardMovementMultiplier;
            else if (!_controller.input.RetrieveJumpHoldInput(this.gameObject) || _body.velocity.y < 0)
                _body.gravityScale = _downwardMovementMultiplier;
            else if (_body.velocity.y == 0)
                _body.gravityScale = _defaultGravityScale;
        }

        _body.velocity = _velocity;
    }

    /// <summary>
    /// Метод прыжка
    /// </summary>
    private void JumpAction()
    {
        if (_coyoteCounter > 0f || (_jumpPhase < _maxAirJumps && IsJumping))
        {
            IsJumping = true;
            if (IsJumping) _jumpPhase += 1;

            _jumpBufferCounter = 0;
            _coyoteCounter = 0;
            _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight * _upwardMovementMultiplier);

            if (_velocity.y > 0f)
                _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
            else if (_velocity.y < 0f)
                _jumpSpeed += Mathf.Abs(_body.velocity.y);

            _velocity.y += _jumpSpeed;
        }
    }
}
