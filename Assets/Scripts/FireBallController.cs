using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour {
	private CharacterController controller;
	private float speed = 1.3f;

	// Update is called once per frame
	void Update () {
		bool forward = transform.root.GetComponent<Player> ().GetIsForward ();

		if (forward) {
			GetComponent<Rigidbody> ().velocity = Vector3.forward * speed;
		} else {
			GetComponent<Rigidbody> ().velocity = Vector3.back * speed;
		}
	}
}
