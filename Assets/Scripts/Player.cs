﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {
	private float _gravity = 1.0f;
	private int maxTpAmmo;
	private Animator anim;
	private bool isForward = true;
	private bool isBackward = false;
    private int deaths = 0;

	private bool teleportRunning = false;

	[Header("La puissance du saut")]
	public float jumpForce = 5f;

	[Header("Vitesse de déplacement horizontal")]
	public float moveSpeed = 5f;

	[Header("Portée du teleport")]
	public float teleportRange = 1f;

	public int tpAmmo = 2;

	[Header("Le prefab de l'attaque")]
	public Transform electricFireBall;

	private CharacterController controller;

	public bool hasGravity = true;


	[Header("Le prefab de l'effet de tp - start")]
	public GameObject tpStartParticles;


	[Header("Le prefab de l'effet de tp - end")]
	public GameObject tpEndParticles;
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

		if (movement.y > 0) {
			anim.Play ("Jump");
		} else if (axis < 0) {
			if (isForward) {
				transform.Rotate (new Vector3 (0, -180, 0));
				isForward = false;
				isBackward = true;
			}

			anim.Play ("Run 1");
		} else if (axis > 0) {
			if (isBackward) {
				transform.Rotate (new Vector3 (0, 180, 0));
				isForward = true;
				isBackward = false;
			}

			anim.Play ("Run 2");
		} else {
			anim.Play ("Idle 1");
		}
	}

	void Teleport() {
		if (!teleportRunning) {
			StartCoroutine ("TeleportCoroutine");
		}
	}

	IEnumerator TeleportCoroutine(){
		this.teleportRunning = true;
		this.hasGravity = false;
		Vector3 direction = new Vector3 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
		Vector3 tpVector = direction.normalized * teleportRange;

		this.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;

		Instantiate (tpStartParticles, transform.position, Quaternion.identity);

		yield return new WaitForSeconds (.2f);

		transform.position += tpVector;

		Instantiate (tpEndParticles, transform.position, Quaternion.identity);
		this.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
		this.tpAmmo--;
		this.teleportRunning = false;	
		this.hasGravity = true;	
	}

	void Shoot() {
		anim.SetTrigger("isAttacking");
		Transform projectile = Instantiate (electricFireBall);
	}

	public void Kill() {
        tpAmmo = maxTpAmmo;
        deaths++;
    }

    public int Deaths()
    {
        return deaths;
    }

    public int TeleportAmmo()
    {
        return tpAmmo;
    }

	public bool GetIsForward() {
		return isForward;
	}

	public bool GetIsBackward() {
		return isBackward;
	}
}
