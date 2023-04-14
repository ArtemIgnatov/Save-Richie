using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private Controller _controller;
    private Animator _playerAnimator;
    private Jump _jumpController;
    private Dash _dashController;
    private Move _moveController;
    private CollisionDataRetriever _ground;
    private Player _player;
    private PlayerAttack _attack;
    private Rigidbody2D _rigidbody;
    private float _sitTime = 2f;

    private void Awake()
    {
        _controller = GetComponent<Controller>();
        _playerAnimator = GetComponent<Animator>();
        _jumpController = GetComponent<Jump>();
        _dashController = GetComponent<Dash>();
        _moveController = GetComponent<Move>();
        _ground = GetComponent<CollisionDataRetriever>();
        _player = GetComponent<Player>();
        _attack = GetComponent<PlayerAttack>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        // ������
        if (_player != null && _player.IsDead)
            _playerAnimator.SetBool("IsDead", true);

        // ������
        if (_jumpController != null && _jumpController.IsJumping)
            _playerAnimator.SetTrigger("Jumping");
        else
            _playerAnimator.ResetTrigger("Jumping");

        // ���
        if (_dashController != null && _dashController.IsDasshing)
            _playerAnimator.SetTrigger("Dashing");
        else
            _playerAnimator.ResetTrigger("Dashing");

        // �������
        if (!_ground.OnGround && !_ground.OnWall && _jumpController.IsFalling && !_dashController.IsDasshing)
            _playerAnimator.SetBool("Falling", true);
        else
            _playerAnimator.SetBool("Falling", false);

        // ���
        if (_moveController != null && _moveController.IsRunning)
            _playerAnimator.SetBool("Running", true);
        else if (!_moveController.IsRunning )
            _playerAnimator.SetBool("Running", false);

        // ���������� �� �����
        if (_ground.OnWall && !_ground.OnGround)
            _playerAnimator.SetBool("WallGrabing", true);
        else if (!_ground.OnWall)
            _playerAnimator.SetBool("WallGrabing", false);

        // �����
        if (_attack.IsAttacking)
            _playerAnimator.SetTrigger("Attacking");
        else
            _playerAnimator.ResetTrigger("Attacking");

        // ������� � ��������� ������
        if (_ground.OnGround && _rigidbody.velocity.x == 0)
        {
            if (!_playerAnimator.GetBool("IsSit"))
                _sitTime -= Time.deltaTime;
            else _sitTime = 2f;

            if (_sitTime <= 0.1f) _playerAnimator.SetBool("IsSit", true);
        }
        else
        {
            _playerAnimator.SetBool("IsSit", false);
            _sitTime = 2f;
        }

    }

    /// <summary>
    /// ��������� �����
    /// </summary>
    public void IsHurting()
    {
        _playerAnimator.SetTrigger("Hurting");
    }
}
