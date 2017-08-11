using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestore : MonoBehaviour {

	public float amount = 100f;
	
	AudioSource restoreSound;

	// Use this for initialization
	void Start () {
		restoreSound = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			CharacterHealth ch = other.gameObject.GetComponent<CharacterHealth>();
			if (ch.maxHealth - ch.health > 0) {
				ch.heal(amount);
				restoreSound.Play();
				Destroy(gameObject, restoreSound.clip.length);
			}
		}
	}
}
