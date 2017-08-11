using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	EnemyUpdate eu;
	EnemyAttack ea;
	EnemyHealth eh;
	Animator anim;
	
	// Use this for initialization
	void Start () {
		eu = transform.parent.GetComponent<EnemyUpdate>();
		eh = transform.parent.GetComponent<EnemyHealth>();
		ea = transform.parent.GetComponent<EnemyAttack>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (eh.alive) {
			faceDirection();

			anim.SetBool("Walking", eu.walking);

			anim.SetBool("Attacking", eu.attacking);
		}
		
		if (eu.deathTrigger) {
			anim.SetTrigger("Death");
		}
		
		
	}
	
	void faceLeft() {
		transform.localScale = new Vector3 (1, 1, 1);
//		anim.transform.Rotate(0, 180, 0);
	}

	void faceRight() {
		transform.localScale = new Vector3 (-1, 1, 1);
//		anim.transform.Rotate(0, 180, 0);
	}
	
	void faceDirection() {
		if (eu.direction.Equals("Right")) {
			faceRight();
		} else if (eu.direction.Equals("Left")) {
			faceLeft();
		}


	}
	
	void thrustFoward() {
		eu.moveFoward();
	}
	
	void callAttack() {
		ea.attack();
	}
	
	void createSmoke() {

	}
	
	void callDeath() {
		eh.death();
	}
}
