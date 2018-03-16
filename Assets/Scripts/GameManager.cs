using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
	private GameOverUI gameOverUI;

    private Player player;
    private StoneMonster stoneMonster;
    private SpikeMonster spikeMonster;

    private Vector3 playerSpawn;
    private Vector3 stoneMonsterSpawn;
    private Vector3 spikeMonsterSpawn;

    private bool gameIsPaused = false;
    private StoppableGameobject[] stoppableGameobjects;

    //private EventSystem eventSystem;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        else if (instance != this) //If instance already exists and it's not this
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        playerSpawn = transform.Find("PlayerSpawn").position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.transform.position = playerSpawn;

        stoneMonsterSpawn = transform.Find("StoneMonsterSpawn").position;
        stoneMonster = GameObject.FindGameObjectWithTag("Monster").GetComponent<StoneMonster>();
        stoneMonster.transform.position = stoneMonsterSpawn;
        /*
        spikeMonsterSpawn = transform.Find("StoneMonsterSpawn").position;
        spikeMonster = GameObject.FindGameObjectWithTag("SpikeMonster").GetComponent<SpikeMonster>();
        spikeMonster.transform.position = spikeMonsterSpawn;
        */

        //eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        gameOverUI = FindObjectOfType(typeof(GameOverUI)) as GameOverUI;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        gameIsPaused = !gameIsPaused;

        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene("PauseMenuScene", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.UnloadSceneAsync("PauseMenuScene");
            Time.timeScale = 1.0f;
        }
    }

    // Reset game objects
    public void GameOver()
    {
        gameOverUI.FadeInOut();

        player.Kill();
        player.transform.position = playerSpawn;
		player.alive = true;

        stoneMonster.Reset();
        stoneMonster.transform.position = stoneMonsterSpawn;
    }
}
