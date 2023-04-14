using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private StateMachine _machine;

    private void Update()
    {
        if (_machine != null && _player != null)
        {
            if (_player.IsDead) StartCoroutine(GameOver());
        }
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        _machine.GameOverCanvasAction();
    }
}
