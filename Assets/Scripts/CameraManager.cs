using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
	public Transform Player;
	private Vector3 Offset = new Vector3(0, 1, -3.5f);

	// Update is called once per frame
	void LateUpdate () {
		if (Player != null) {
			transform.position = Player.position + Offset;
		}
	}
}
