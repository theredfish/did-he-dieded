using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    private int level = 3;                                 //Current level number, expressed in game as "Day 1".
	private GameOverUI gameOverUI;

	public Vector3 spawnLocation;							// Spawn location for player

    //Awake is always called before any Start functions
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

        gameOverUI = FindObjectOfType(typeof(GameOverUI)) as GameOverUI;
    }

    //Initializes the game for each level.
    public void GameOver()
    {
        gameOverUI.FadeInOut();

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.transform.position = spawnLocation;
		player.Kill();

        StoneMonster stoneMonster = GameObject.FindGameObjectWithTag("Monster").GetComponent<StoneMonster>();
        stoneMonster.Reset();
    }
}
