using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    GameManager gameManager;

    void Awake()
    {
        GameObject gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    public void Resume()
    {
        gameManager.TogglePause();
        SceneManager.UnloadSceneAsync("PauseMenuScene");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
