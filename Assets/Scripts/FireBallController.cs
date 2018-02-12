using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour {
	private CharacterController player;
	private float speed = 1.3f;
	private bool toRight;

	void Awake() {
		this.player = gameObject.GetComponentInParent<CharacterController> ();
		Vector3 offset = new Vector3 (0, 1, 0);
		transform.position = player.gameObject.transform.position + offset;
	}

	void Start() {
	}

	// Update is called once per frame
	void Update () {
		Vector3 velocity;

		if (player.gameObject.GetComponent<Player> ().GetIsForward ()) {
			velocity = new Vector3(0,0,1) * speed;
		} else {
			velocity = new Vector3(0,0,-1) * speed;
		}

		GetComponent<Rigidbody> ().velocity = velocity;
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag != "Player") {
			Destroy (gameObject);
		}
	}
}
