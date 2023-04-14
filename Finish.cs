using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Finish : MonoBehaviour
{
    [SerializeField] private StateMachine _stateMachine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.TryGetComponent(out Player player);
            collision.gameObject.TryGetComponent(out PlayerAnimationController animationController);
            player.DisableController();
            StartCoroutine(FinishAction());
        }
    }

    private IEnumerator FinishAction()
    {
        yield return new WaitForSeconds(3f);
        _stateMachine.FinishCanvasActivate();
    }
}
