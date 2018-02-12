using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour {

    private bool isPaused = false; // Permet de savoir si le jeu est en pause

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
		
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
	}

    void OnGUI()
    {
        if (isPaused)
        {
            if (GUI.Button(new Rect(Screen.width / 2 + 40, Screen.height / 2 - 20, 100, 40), "Continuer"))
            {
                isPaused = false;
            }
            if (GUI.Button(new Rect(Screen.width / 2 + 40, Screen.height / 2 + 40, 100, 40), "Menu principal"))
            {
                Application.LoadLevel("WelcomeScreen");
            }
            if (GUI.Button(new Rect(Screen.width / 2 + 40, Screen.height / 2 + 100, 100, 40), "Fuire"))
            {
                Application.Quit();
            }
        }
    }
}
