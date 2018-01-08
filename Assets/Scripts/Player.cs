using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

	private float maxTpAmmo;

	[Header("La puissance du saut")]
	public float jumpForce = 5f;

	[Header("Vitesse de déplacement horizontal")]
	public float moveSpeed = 5f;

	[Header("Portée du teleport")]
	public float teleportRange = 1f;

	public float tpAmmo = 2f;

	private CharacterController controller;

	public bool hasGravity = true;

	// The current movement vector
	private Vector3 movement = Vector3.zero;

	// Use this for initialization
	void Start () {
		this.controller = GetComponent<CharacterController> ();
		this.maxTpAmmo = this.tpAmmo;
	}
	
	// Update is called once per frame
	void Update () {
		// First of all, we get the base movement
		movement.x = Input.GetAxis("Horizontal") * this.moveSpeed;
		// Then we check for a TP.
		if (Input.GetButtonDown("Teleport") && this.tpAmmo >= 1f) {
			this.Teleport ();
		}
		// Jump
		if (Input.GetButton("Jump") && this.controller.isGrounded) {
			movement.y = this.jumpForce;
		}
		if (!this.controller.isGrounded && this.hasGravity) {
			movement.y += Physics.gravity.y * Time.deltaTime;
		}
		this.controller.Move (movement * Time.deltaTime);
	}

	void Teleport() {
		Vector3 direction = new Vector3 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"), 0);
		Vector3 tpVector = direction.normalized * teleportRange;
		transform.position += tpVector;
		this.tpAmmo--;
	}

	public void Fall() {
		transform.position = Vector3.zero;
		this.tpAmmo = maxTpAmmo;
	}
}
