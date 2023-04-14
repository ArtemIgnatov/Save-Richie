using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private StateMachine _stateMachine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.TryGetComponent(out Player player);
            player.DisableController();
            StartCoroutine(GameOverAction());
        }
    }

    private IEnumerator GameOverAction()
    {
        yield return new WaitForSeconds(1f);

        _stateMachine.GameOverCanvasAction();
    }
}

