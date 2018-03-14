using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DeathCollider : MonoBehaviour {

	void OnTriggerEnter(Collider other){
        Debug.LogError(other);
		if (other.CompareTag ("Player")) {
            if (!(this.CompareTag("Monster") && this.GetComponent<StoneMonster>().isDead))
            {
                GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
                gameManager.GameOver();
            }
		}
	}
}
