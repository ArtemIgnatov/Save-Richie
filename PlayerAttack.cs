using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Controller))]

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _defaultAttackArea;
    [SerializeField] private GameObject _jumpAttackArea;
    [SerializeField] private float _timeToAttack = 0.5f;
    private Controller _controller;
    private Jump _jump;

    private bool _desiredAttack;
    public bool IsAttacking { get; private set; }

    private void Awake()
    {
        IsAttacking = false;
        _controller = GetComponent<Controller>();
        _jump = GetComponent<Jump>();
        _jumpAttackArea.SetActive(false);
    }

    private void Update()
    {
        if (_controller.input != null)
        {
            _desiredAttack = _controller.input.RetrieveAttackInput(this.gameObject);
            if (_desiredAttack && !IsAttacking) StartCoroutine(Attacking());    
        }
    }

    private void FixedUpdate()
    {
        _jumpAttackArea.SetActive(_jump.IsFalling);
    }

    private IEnumerator Attacking()
    {

        IsAttacking = true;
        _defaultAttackArea.SetActive(IsAttacking);

        yield return new WaitForSeconds(_timeToAttack);

        IsAttacking = false;
        _defaultAttackArea.SetActive(IsAttacking);
    }
}

