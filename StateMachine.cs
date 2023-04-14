using UnityEngine;
using UnityEngine.SceneManagement;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;
    [SerializeField] private GameObject _pauseCanvas;
    [SerializeField] private GameObject _finishCanvas;
    [SerializeField] private GameObject _gameOverCanvas;
    private bool _paused = false;

    // Update is called once per frame
    void Update()
    {
        if (_controller != null && _controller.Pause())
        {
            _paused = !_paused;
            Pause(_paused);
        }
    }


    private void Pause(bool isPaused)
    {
        if (isPaused == true)
        {
            _pauseCanvas.SetActive(isPaused);
            Time.timeScale = 0;
        }
        else if (isPaused == false) 
        {
            _pauseCanvas.SetActive(isPaused);
            Time.timeScale = 1;
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void FinishCanvasActivate()
    {
        if (_finishCanvas != null)
        {
            _finishCanvas.SetActive(true);
        }
    }

    public void GameOverCanvasAction()
    {
        if (_gameOverCanvas != null)
        {
            _gameOverCanvas.SetActive(true);
        }
    }
}
