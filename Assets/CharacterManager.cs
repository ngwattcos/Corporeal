using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using System
//using System;

public class CharacterManager : MonoBehaviour {

	Animator anim;
	SpriteRenderer sr;

	CharacterUpdate characterUpdate;
	CharacterHealth characterHealth;
	
	
	GameObject body;
	
	public int stepNum = 0;
	public int totalStepNums = 4;
	
	
	public AudioSource runningSound;
	public AudioClip runningSoundClip;
	
	public bool mouseMoved = false;
	
//	public AudioClip audioClip;

//	int direction = 1;

	// Use this for initialization
	void Start() {
		body = GameObject.FindGameObjectWithTag("Body");
		anim = GetComponent<Animator>();
//		sr = GetComponent<SpriteRenderer>();
		characterUpdate = transform.parent.GetComponent<CharacterUpdate>();
		characterHealth = transform.parent.GetComponent<CharacterHealth>();
		
//		audio = GetComponent<AudioSource>();
		runningSound = gameObject.AddComponent<AudioSource>();
		runningSound.clip = runningSoundClip;
		runningSound.playOnAwake = false;
	}
	
	void Update() {
		mouseMoved = Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0;
	}
	
	// 1 = most, -1 = least
	// use Math.Abs(heading)
	float heading = 0.0f;
	bool headingIsPositive = false;
	
	float angle = 0.0f;
	
	float maxRotation = 60f;
	
	float degToRad = 180f/Mathf.PI;
	
	float radToDeg = Mathf.PI/180f;
	
	bool facingRight = false;

	// Update is called once per frame
	void LateUpdate() {
//		body.transform.LookAt(new Vector3(50f, 0f, 0f));
		
		Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		pos.z = 0;
		
		angle = Mathf.Atan2(pos.y - body.transform.position.y, pos.x - body.transform.position.x);
//		Debug.Log(angle);
		
		headingIsPositive = angle > 0;
//		Debug.Log(headingIsPositive);
		
		anim.SetBool("HeadingIsPositive", headingIsPositive);
		
		

		if (characterHealth.health > 0) {
			
			if (mouseMoved) {
				
				if (Mathf.Abs(angle) > Mathf.PI/2f) {
					characterUpdate.direction = "Left";
					facingRight = false;
				} else {
					characterUpdate.direction = "Right";
					facingRight = true;
				}
				
				float value = angle / (Mathf.PI * 0.5f);

				
				
				float layer1Val = Mathf.Max(value, 0f);
				float layer2Val = Mathf.Min(value, 0);
//				Debug.Log("value: " + value + ", layer1: " + layer1Val + ", layer2: " + layer2Val);
				
				if (Mathf.Abs(value) < 1) {
					if (value > 0.7f) {
						value = 0.7f;
					}
					anim.SetLayerWeight(1, (float) Mathf.Abs(value));
				} else {
					float value2 = value;
					if (value > 0) {
						value2 = (-(value - 1)) + 1;
					} else {
						value2 = (-(value + 1)) - 1;
					}
					
					if (value2 > 0.7f) {
						value2 = 0.7f;
					}
					
					anim.SetLayerWeight(1, (float) Mathf.Abs(value2));
				}
				
//				anim.SetLayerWeight(2, layer2Val);
			} else {
//				anim.SetLayerWeight(1, 0);
			}
			
			anim.SetBool("Walking", characterUpdate.walking && characterHealth.health > 0 && !characterUpdate.falling);


			faceDirection();
			
			anim.SetBool("Alive", characterHealth.health > 0);

			anim.SetBool("Grounded", characterUpdate.grounded);
			
			if (characterUpdate.grounded) {
				
			}
			
			
			anim.SetBool("Rocketing", (characterUpdate.primaryRocket || characterUpdate.secondaryRocket) && characterHealth.health > 0);


			anim.SetBool("Crouching", characterUpdate.crouching);


			if (characterUpdate.jumping) {
				anim.SetTrigger("Jump");
			}
			
			if (characterUpdate.fallTrigger) {
				anim.SetTrigger("Fall");
			}

			anim.SetBool("Falling", characterUpdate.falling && characterHealth.health > 0);



			if (!characterUpdate.grounded) {
				//			anim.SetTrigger("Land");
			}
		} else {
			anim.SetBool("Alive", false);
			anim.SetBool("Rocketing", false);
			anim.SetBool("Falling", false);
		}
		
		/*
		if (characterUpdate.D) {
			characterUpdate.walking = true;
			faceRight();
		} else {
			anim.SetBool("Walking", false);
		}

		if (characterUpdate.A) {
			anim.SetBool("Walking", true);
			faceLeft();
		} else {
			anim.SetBool("Walking", false);
		}

		if (characterUpdate.Space) {
			
			anim.SetBool("Walking", true);
		}
		
		if (characterUpdate.X) {
			anim.SetTrigger("Death");
		}*/
	}

	void faceLeft() {
//		transform.localRotation = Quaternion.Euler(0, 180, 0);
		transform.localScale = new Vector3 (-1, 1, 1);
	}

	void faceRight() {
		transform.localScale = new Vector3 (1, 1, 1);
//		transform.localRotation = Quaternion.Euler(0, 0, 0);
//		transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
	
	void faceDirection() {
		if (characterUpdate.direction.Equals("Right")) {
			faceRight();
		} else if (characterUpdate.direction.Equals("Left")) {
			faceLeft();
		}
		
		
	}
	
	void createSmoke() {
		
	}
	
	void playStepSound() {
		if (characterUpdate.grounded) {
			float factor = Random.Range(0.5f, 0.8f);
//			runningSound.volume = factor * factor * 2;
			runningSound.volume = 1.0f;
			runningSound.pitch = factor;
			runningSound.Play();
		}
		
//		AudioSource.PlayClipAtPoint(audioClip, (Vector3) characterUpdate.transform.position);
		
	}
	
	void throwItem() {
		
	}
	
	void death() {
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadSceneAsync(scene.name);
	}
}
