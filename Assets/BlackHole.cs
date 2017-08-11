using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackHole : MonoBehaviour {
	
	public Text passives;
	public Text passiveAggressives;
	
	
	public int totalCaptured = 0;
	public int capturedPassives = 0;
	
	// should all be grouped together
	// public int capturedPassiveAggressives = 0;
	
	// bosses should not be able to be captured
	// public int capturedBosses = 0;
	
	Switch sw;
	
	AudioSource suck;
	
	CharacterUpdate cu;
	
	// Use this for initialization
	void Start () {
		sw = transform.parent.GetComponent<Switch>();
		cu = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterUpdate>();
	}
	
	// Update is called once per frame
	void Update () {
		passives.text = "x " + capturedPassives;
		suck = GetComponent<AudioSource>();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (sw.getEnabled() && Input.GetButton("Fire1") &&
			other.gameObject.GetComponent<PassiveUpdate>() != null) {
			GameObject pass = other.gameObject;
			capturedPassives += 1;
			totalCaptured += 1;
			Destroy(pass);
			suck.Play();
			
		}
		
		if (sw.getEnabled() && Input.GetButton("Fire1") &&
			other.gameObject.GetComponent<PassiveAggressiveUpdate>() != null) {
			GameObject pass = other.gameObject;
			capturedPassives += 1;
			totalCaptured += 1;
			Destroy(pass);
			suck.Play();

		}
		
		if (sw.getEnabled() && Input.GetButton("Fire1") &&
			other.gameObject.GetComponent<PassivePlantUpdate>() != null) {
			GameObject pass = other.gameObject;
			capturedPassives += 1;
			totalCaptured += 1;
			Destroy(pass);
			suck.Play();

		}
		
		/*if (other.gameObject.GetComponent<PassiveAggressiveUpdate>() != null) {
			GameObject pass = other.gameObject;
			capturedPassiveAggressives += 1;
			Destroy(pass);
		}*/
		
		/*if (other.gameObject.GetComponent<EnemyUpdate>() != null) {
			GameObject boss = other.gameObject;
			capturedBosses += 1;
			Destroy(boss);
		}*/
	}
}
