using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	CharacterHealth ch;
	EnemyUpdate eu;
	
	public float attackTime = 1.0f;
	
	
	// Use this for initialization
	void Start () {
		eu = GetComponent<EnemyUpdate>();
		ch = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		if (eu.attacking) {
			
		}
	}
	
	public void attack() {
		if (Vector3.Distance(transform.position, eu.target.position) <= 4f) {
			ch.damage(10f);
		}
		
	}
}
