using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
	public Text tpAmmoText;
    public Text deathCounter;
	private Player player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update () {
		tpAmmoText.text = "X " + player.TeleportAmmo().ToString();
        deathCounter.text = "X " + player.Deaths().ToString();
    }
}
