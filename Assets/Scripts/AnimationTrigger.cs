using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AnimationTrigger : MonoBehaviour {

	public Animator animator;

	public string stateName;

	public void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			animator.Play(stateName);
		}
	}
}
