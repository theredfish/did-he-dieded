using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DeathCollider : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
            if (!(this.CompareTag("Monster") && this.GetComponent<StoneMonster>().isDead))
            {
				other.GetComponent<Animator> ().Play ("Death");
				other.GetComponent<Player> ().alive = false;
				Invoke ("Kill", 1);
            }
		}
	}

	void Kill(){
		GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		gameManager.GameOver();		
	}
}
