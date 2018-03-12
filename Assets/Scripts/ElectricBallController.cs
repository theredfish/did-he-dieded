using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBallController : MonoBehaviour {
	private GameObject player;
	private GameObject startPosition;
	private float speed = 8f;
	private bool isForward = true;

	void Awake() {
		this.player = GameObject.FindGameObjectWithTag ("Player");
		this.startPosition = GameObject.FindGameObjectWithTag ("StartAttackPosition");
	}

	// Update is called once per frame
	void Start () {
		transform.position = this.startPosition.transform.position;
		isForward = player.gameObject.GetComponent<Player> ().GetIsForward ();
	}

	void Update() {
		if (isForward) {
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		} else {
			transform.Translate (Vector3.left * speed * Time.deltaTime);
		}
	}
		
	void OnTriggerEnter(Collider other) {
		if (other.tag != "Player") {
			Destroy (gameObject);
		}
	}
}	