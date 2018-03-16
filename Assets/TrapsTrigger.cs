using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class TrapsTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			this.PikesOut();
		}
	}

	void PikesOut(){
		GetComponent<Animator> ().Play("PikesOut");
	}

	public void PikesIn(){
		GetComponent<Animator> ().Play("PikesIn");
	}
}
