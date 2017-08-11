using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyFloor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<CharacterHealth>() != null) {
			CharacterHealth ch = other.GetComponent<CharacterHealth>();
			ch.beKilled();
		}
		
		if (other.GetComponent<EnemyHealth>() != null) {
			EnemyHealth eh = other.GetComponent<EnemyHealth>();
			eh.beKilled();
		}
	}
}
