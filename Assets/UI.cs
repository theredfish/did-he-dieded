using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public Text tpAmmoText;

	public Player player;
	
	// Update is called once per frame
	void Update () {
		tpAmmoText.text = "TP: " + Mathf.Ceil(player.tpAmmo).ToString();
	}
}
