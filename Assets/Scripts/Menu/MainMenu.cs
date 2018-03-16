using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    GameManager gameManager;
	void Awake () {
        gameManager = GetComponent<GameManager>();
	}

    public void StartGame()
    {
        SceneManager.LoadScene("newChar");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}