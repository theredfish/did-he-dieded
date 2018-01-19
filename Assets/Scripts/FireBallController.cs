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
			GetComponent<Rigidbody> ().velocity = new Vector3(1, 0, 0) * speed;
		} else {
			GetComponent<Rigidbody> ().velocity = new Vector3(-1, 0, 0) * speed;
		}
	}
}
