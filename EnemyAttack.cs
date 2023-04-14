using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Controller))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject _attackArea;
    [SerializeField] private float _timeToAttack = 0.25f;

    private Controller _controller;
    private float _cooldown = 1f;
    private bool _canAttack, _desiredAttack;
    public bool IsAttacking { get; private set; }

    private void Awake()
    {
        _canAttack = true;
        IsAttacking = false;
        _controller = GetComponent<Controller>();
    }

    private void Update()
    {
        if (_controller.input != null)
        {
            _desiredAttack = _controller.input.RetrieveAttackInput(this.gameObject);

            if (_desiredAttack && !IsAttacking && _canAttack) StartCoroutine(Attacking());
        }
    }

    private IEnumerator Attacking()
    {
        _canAttack = false;
        IsAttacking = true;

        //Задержка перед атакой
        yield return new WaitForSeconds(0.5f);

        _attackArea.SetActive(IsAttacking);
        IsAttacking = false;

        yield return new WaitForSeconds(_timeToAttack);
        _attackArea.SetActive(IsAttacking);

        yield return new WaitForSeconds(_cooldown);
        _canAttack = true;
    }
}
