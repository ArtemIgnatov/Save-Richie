using UnityEngine;

[RequireComponent(typeof(Controller), typeof(CollisionDataRetriever), typeof(Rigidbody2D))]
public class WallInteractor : MonoBehaviour
{
    public bool WallJumping { get; private set; }

    [Header("Wall Slide")]
    [SerializeField][Range(0f, 5f)] private float _wallSlideMaxSpeed = 2f;
    [Header("Wall Jump")]
    [SerializeField] private Vector2 _wallJumpClimb = new Vector2(2f, 5f);
    [SerializeField] private Vector2 _wallJumpBounce = new Vector2(6f, 6f);
    [SerializeField] private Vector2 _wallJumpLeap = new Vector2(6f, 3f);
    [Header("Wall Stick")]
    [SerializeField, Range(0.05f, 1f)] private float _wallStickTime = 0.25f;


    private CollisionDataRetriever _ground;
    private Rigidbody2D _body;
    private Controller _controller;

    private Vector2 _velocity;
    private bool _onWall, _onGround, _desiredJump;
    private float _wallDirectionX, _wallStickCounter;

    // Start is called before the first frame update
    void Start()
    {
        _ground = GetComponent<CollisionDataRetriever>();
        _body = GetComponent<Rigidbody2D>();
        _controller = GetComponent<Controller>();
    }

    void Update()
    {
        if (_controller.input != null && _onWall && !_onGround)
            _desiredJump |= _controller.input.RetrieveJumpInput(this.gameObject);
    }

    private void FixedUpdate()
    {
        _velocity = _body.velocity;
        _onWall = _ground.OnWall;
        _onGround = _ground.OnGround;
        _wallDirectionX = _ground.ContactNormal.x;

        #region Wall Slide

        if (_onWall && _velocity.y < -_wallSlideMaxSpeed)
            _velocity.y = -_wallSlideMaxSpeed;

        #endregion

        #region Wall Stick

        // Задаем условие, при котором персонаж не будет терять контакт со стеной и падать вниз,
        // во время ввода с клавиатуры, если мы захотим прыгнуть в противоположном от стены направлении

        if (_ground.OnWall && !_ground.OnGround && !WallJumping)
        {
            if (_wallStickCounter > 0)
            {
                _velocity.x = 0;

                if (_controller.input != null && _controller.input.RetrieveMoveInput(this.gameObject) == _ground.ContactNormal.x)
                    _wallStickCounter -= Time.deltaTime;
                else
                    _wallStickCounter = _wallStickTime;
            }
            else
                _wallStickCounter = _wallStickTime;
        }
        #endregion

        #region Wall Jump

        if ((_onWall && _velocity.x == 0) || _onGround)
            WallJumping = false;

        if (_desiredJump)
        {
            // Условие при котором игрок отталкивается от стены с направлением в сторону стены,
             // тем самым как бы карапкается по стене

            
            // Усовие при котором игрок отталкивается от стены без ввода направления
            if (_controller.input.RetrieveMoveInput(this.gameObject) == 0)
            {
                _velocity = new Vector2(_wallJumpBounce.x * _wallDirectionX, _wallJumpBounce.y);
                WallJumping = true;
                _desiredJump = false;
            }
            else if (-_wallDirectionX == _controller.input.RetrieveMoveInput(this.gameObject))
            {
                _velocity = new Vector2(_wallJumpClimb.x * _wallDirectionX, _wallJumpClimb.y);
                WallJumping = true;
                _desiredJump = false;
            }
            // Условие при котором игрок как бы спрыгивает со стены в противоположном нправлнии
            else
            {
                _velocity = new Vector2(_wallJumpLeap.x * _wallDirectionX, _wallJumpLeap.y);
                WallJumping = true;
                _desiredJump = false;
            }


        }
        #endregion

        _body.velocity = _velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _ground.EvaluateCollision(collision);

        if (_ground.OnWall && !_ground.OnGround && WallJumping)
            _body.velocity = Vector2.zero;
    }
}
