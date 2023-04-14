using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Dash : MonoBehaviour
{
    [SerializeField, Range(0f, 30f)] private float _dashingPower = 15f;
    [SerializeField, Range(0f, 1f)] private float _dashingTime = 0.2f;
    [SerializeField] private TrailRenderer _dashingTrailRenderer = null;
    
    private Controller _controller;
    private Rigidbody2D _body;
    private Vector2 _velocity;

    private float _dashingCooldown = 1f;
    private bool _desiredDash, _canDassh;
    public bool IsDasshing { get; private set; }

    void Awake()
    {
        _canDassh = true;
        _body = GetComponent<Rigidbody2D>();
        _controller = GetComponent<Controller>();
    }

    void Update()
    {
        if (_controller.input != null)
        {
            _desiredDash = _controller.input.RetrieveDashInput(this.gameObject);
            if (IsDasshing) return;
            if (_desiredDash && _canDassh) StartCoroutine(Dashing());
        }
    }

    private void FixedUpdate()
    {
        if (IsDasshing) return;
        _velocity = _body.velocity;
    }

    private IEnumerator Dashing()
    {
        _canDassh = false;
        IsDasshing = true;

        _body.gravityScale = 0;
        _body.velocity = new Vector2(transform.localScale.x * _dashingPower, 0f);
        _dashingTrailRenderer.emitting = true;

        yield return new WaitForSeconds(_dashingTime);
        _dashingTrailRenderer.emitting = false;
        IsDasshing = false;

        yield return new WaitForSeconds(_dashingCooldown);
        _canDassh = true;
    }
}
