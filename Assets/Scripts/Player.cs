using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {
	private float _gravity = 1.0f;
	private float maxTpAmmo;
	private Animator anim;
	private bool isForward = true;
	private bool isBackward = false;

	[Header("La puissance du saut")]
	public float jumpForce = 5f;

	[Header("Vitesse de déplacement horizontal")]
	public float moveSpeed = 5f;

	[Header("Portée du teleport")]
	public float teleportRange = 1f;

	public float tpAmmo = 2f;

	[Header("Le prefab de l'attaque")]
	public Transform electricFireBall;

	private CharacterController controller;

	// The current movement vector
	private Vector3 movement = Vector3.zero;

	// Use this for initialization
	void Start () {
		this.controller = GetComponent<CharacterController> ();
		this.maxTpAmmo = this.tpAmmo;
		this.anim = GetComponent<Animator>();
	}

	void Update () {
		// First of all, we get the base movement
		movement.x = Input.GetAxis("Horizontal") * this.moveSpeed;

		// Then we check for a TP.
		if (Input.GetButtonDown("Teleport") && this.tpAmmo >= 1f) {
			this.Teleport ();
		}

		// We check any attack
		if (Input.GetButtonDown("Fire2")) {
			this.Shoot();
		}

		// Then we get the vertical movement
		if (controller.isGrounded) {
			movement.y = 0;

			// Jump
			if (Input.GetButton("Jump") && this.controller.isGrounded) {
				movement.y = jumpForce;
			}

		} else {
			movement.y -= _gravity;
		}

		WalkOrIdle (movement);
	}

	void WalkOrIdle(Vector3 movement) {
		this.controller.Move(movement * Time.deltaTime);
		float axis = Input.GetAxisRaw("Horizontal");

		if (axis < 0) {
			if (isForward) {
				transform.Rotate(new Vector3(0, -180, 0));
				isForward = false;
				isBackward = true;
			}

			anim.SetTrigger("isWalking");
		} else if (axis > 0) {
			if (isBackward) {
				transform.Rotate (new Vector3 (0, 180, 0));
				isForward = true;
				isBackward = false;
			}

			anim.SetTrigger("isWalking");
		} else {
			anim.SetTrigger("isIdle");
		}
	}

	void Teleport() {
		Vector3 direction = new Vector3 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
		Vector3 tpVector = direction.normalized * teleportRange;

		transform.position += tpVector;
		this.tpAmmo--;
	}

	void Shoot() {
		anim.SetTrigger("isAttacking");
		Transform projectile = Instantiate (electricFireBall);
	}

	public void Fall() {
		transform.position = Vector3.zero;
		this.tpAmmo = maxTpAmmo;
	}

	public bool GetIsForward() {
		return isForward;
	}

	public bool GetIsBackward() {
		return isBackward;
	}
}
