using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System;
using UnityEngine.SocialPlatforms;

public class CharacterHealth : MonoBehaviour {
	
	public float maxHealth = 100f;
	public float health = 100f;
	public bool alive = true;
	
	CharacterUpdate cu;
	
	public AudioSource painSound;
	public AudioClip painSoundClip1;
	public AudioClip painSoundClip2;
	public AudioClip painSoundClip3;
	public AudioClip painSoundClip4;
	public AudioClip[] painSoundClips;
	
	public RectTransform hb;
	
	public Animator animator;
	
	public void damage(float damage) {
		
		health -= damage;
		
		if (health > 0 && damage > 0) {
			int idx = (int) UnityEngine.Random.Range(0, 4f - 0.1f);

			painSound.clip = painSoundClips[idx];
			painSound.pitch = UnityEngine.Random.Range(0.7f, 0.8f);
			painSound.volume = 0.5f;
			painSound.Play();
		} else {
			// death sound
		}
		
	}
	
	public void heal(float amount) {
		health += amount;
	}
	
	public bool isAlive() {
		return health > 0;
	}
	
	
	public void Start() {
		hb = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<RectTransform>();
		cu = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterUpdate>();
		
		painSound = gameObject.AddComponent<AudioSource>();
		
		painSoundClips = new AudioClip[4];
		
		painSoundClips[0] = painSoundClip1;
		painSoundClips[1] = painSoundClip2;
		painSoundClips[2] = painSoundClip3;
		painSoundClips[3] = painSoundClip4;
		
		
	}
	
	public void Update() {
		if (health > maxHealth) {
			health = maxHealth;
		}
		
		// set margin for healthbar
		hb.offsetMax = new Vector2((100 - health) * -4f, 0);
		
		if (alive && health <= 0) {
			alive = false;
			animator.SetTrigger("Death");
		}
		if (health > 0) {
			alive = true;
		}
	}
	
	public void beKilled() {
		damage(maxHealth);
	}
	
}
