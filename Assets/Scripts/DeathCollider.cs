using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DeathCollider : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			Debug.Log ("KILL !");
            if (!(this.CompareTag("Monster") && this.GetComponent<Monster_behaviour>().isDead))
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().RespawnPlayer();
            }
		}
	}
}
