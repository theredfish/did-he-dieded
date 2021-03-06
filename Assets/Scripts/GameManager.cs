﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    private int level = 1;
	private GameOverUI gameOverUI;

    private Player player;
    private StoneMonster stoneMonster;
    
    private Vector3 playerSpawn;
    private Vector3 stoneMonsterSpawn;


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

        gameOverUI = FindObjectOfType(typeof(GameOverUI)) as GameOverUI;
    }

    // Reset game objects
    public void GameOver()
    {
        gameOverUI.FadeInOut();

        player.Kill();
        player.transform.position = playerSpawn;

        stoneMonster.Reset();
        stoneMonster.transform.position = stoneMonsterSpawn;
    }
}
