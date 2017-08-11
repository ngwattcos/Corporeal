using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenRestore : MonoBehaviour {
	
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
			CharacterUpdate cu = other.gameObject.GetComponent<CharacterUpdate>();
			if (cu.maxRocketFuel - cu.rocketFuel > amount/4f) {
				cu.rocketFuel += amount;
				restoreSound.Play();
				Destroy(gameObject, restoreSound.clip.length);
			}
		}
	}
}
