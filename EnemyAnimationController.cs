using UnityEngine;
[RequireComponent(typeof(Animator))]

public class EnemyAnimationController : MonoBehaviour
{
    private Animator _enemyAnimator;
    private Move _moveController;
    private EnemyAttack _enemyAttackController;
    private Enemy _enemy;

    private void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
        _moveController = GetComponent<Move>();
        _enemyAttackController = GetComponent<EnemyAttack>();
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (_enemyAnimator != null)
        {
            // ������
            if (_enemy.IsDead)
                _enemyAnimator.SetBool("IsDead", true);

            // ��������
            if (_moveController != null && _moveController)
                _enemyAnimator.SetBool("Walking", true);
            else
                _enemyAnimator.SetBool("Walking", false);

            // �����
            if (_enemyAttackController != null && _enemyAttackController.IsAttacking)
                _enemyAnimator.SetTrigger("Attacking");
            else
                _enemyAnimator.ResetTrigger("Attacking");
        }
    }

    /// <summary>
    /// ��������� �����
    /// </summary>
    public void IsHurting()
    {
        _enemyAnimator.SetTrigger("Hurting");
    }
}
