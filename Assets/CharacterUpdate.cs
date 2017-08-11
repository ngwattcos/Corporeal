using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CharacterUpdate : MonoBehaviour {
	
	CharacterHealth ch;
	
	public bool W = false;
	public bool A = false;
	public bool S = false;
	public bool D = false;
	
	public bool F = false;
	public bool L = false;
	
	public bool UP = false;
	public bool DOWN = false;
	public bool LEFT = false;
	public bool RIGHT = false;
	
	
	public int RIGHTCounter = 0;
	public int LEFTCounter = 0;
	
	public bool X = false;
	public bool Space = false;
	
	public bool inContact = false;
	
	public bool grounded = false;
	public bool falling = false;
	public bool walking = false;
	public bool crouching = false;
	public bool jumping = false;
	public bool landing = false;
	
	public float speed;
	public float speedX;
	public float jumpSpeedY;
	
	public string activeWeaponName;
	public GameObject activeWeapon;
	public int weaponIndex = 0;
	
	public string[] weaponsString;
	public Dictionary<string, GameObject> weapons;
//	public Dictionary<string, GameObject> ownedWeapons;
	public List<GameObject> ownedWeapons;
	
	public int points = 0;
	
	Text weaponNameText;
	
	public Transform prevTransform;
	
	// a list of objects tagged "Ground"
	public List<GameObject> groundCollisions;
	
	GameObject pickupPanel;
	Text pickupText;
	
	public float distToGround;
	
	public String direction;
	
	Rigidbody2D rb;
	
	public Animator anim;
	
	
	public AudioSource collisionSound;
	public AudioClip collisionSoundClip;
	
	public AudioSource idleBreathingSound;
	public AudioClip idleBreathingSoundClip;
	public Boolean normalBreathing = false;
	
	public AudioSource exhaleSound;
	public AudioClip exhaleSoundClip;
	
	public AudioSource beepSound;
	public AudioClip beepSoundClip;
	
	public Boolean primaryRocket = false;
	public AudioSource rocketSound;
	public AudioClip rocketSoundClip;
	
	public Boolean secondaryRocket = false;
	public AudioSource secondaryRocketSound;
	public AudioClip secondaryRocketSoundClip;
	public float rocketFuel = 1000f;
	public float maxRocketFuel = 1000f;
	
	public RectTransform tb;
	
	public Boolean fallTrigger = false;
	
	
	// Animator anim;

	void Awake() {
		QualitySettings.vSyncCount = 0;  // VSync must be disabled
		Application.targetFrameRate = 30;
	}
	
	// Use this for initialization
	void Start () {
		
//		Physics2D.IgnoreLayerCollision(11, 9);
		Physics2D.IgnoreLayerCollision(11, 13);
		
		anim = GameObject.FindGameObjectWithTag("CharacterRig").GetComponent<Animator>();
		
		tb = GameObject.FindGameObjectWithTag("OxygenBar").GetComponent<RectTransform>();
		
		ch = GetComponent<CharacterHealth>();
		rb = GetComponent<Rigidbody2D>();
		direction = "Right";
		
		grounded = false;
		falling = false;
		walking = true;
		
		speed = 0f;
		speedX = 10f;
		jumpSpeedY = 10f;
		
		// anim = GetComponentInChildren<Animator>();
		
		pickupPanel = GameObject.FindGameObjectWithTag("PickupPanel");
		pickupPanel.SetActive(false);
		pickupText = GameObject.FindGameObjectWithTag("PickupText").GetComponent<Text>();
		pickupText.enabled = false;
		
		// Declare groundCollisions
		groundCollisions = new List<GameObject>();
		
		// Physics.IgnoreLayerCollision(6, 7, true);
		prevTransform = transform;
		
		weaponsString = new string[] {"Gravity Staff", "EMP Grenade", "Quantum Scanner", "Cathode Ray Gun"};
		weapons = new Dictionary<string, GameObject>();
		// ownedWeapons = new Dictionary<string, GameObject>();
		ownedWeapons = new List<GameObject>();
		
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		
		// scan for weapons inside the hand an dstore them
		foreach (Transform child in allChildren) {
			if (child.gameObject.tag == "Weapon") {
				Debug.Log("Checking child '" + child.gameObject.GetComponent<GenericWeapon>().weaponName + "'");
				weapons.Add(child.gameObject.GetComponent<GenericWeapon>().weaponName, child.gameObject);
			}
		}
		
		
		disableAllWeapons();
		
		
		// attach an empty weapon
		GameObject noWeapon = new GameObject("No Weapon");
		noWeapon.AddComponent<GenericWeapon>();
		noWeapon.GetComponent<GenericWeapon>().weaponName = "No Weapon";
		
		ownedWeapons.Add(noWeapon);
		
		activeWeapon = noWeapon;
		
		weaponNameText = GameObject.FindGameObjectWithTag("WeaponNameText").GetComponent<Text>();
		weaponNameText.text = activeWeapon.GetComponent<GenericWeapon>().weaponName;
		
		collisionSound = gameObject.AddComponent<AudioSource>();
		collisionSound.clip = collisionSoundClip;
		collisionSound.playOnAwake = false;
		
		idleBreathingSound = gameObject.AddComponent<AudioSource>();
		idleBreathingSound.clip = idleBreathingSoundClip;
		idleBreathingSound.playOnAwake = false;
		idleBreathingSound.loop = true;
		normalBreathing = true;
		
		exhaleSound = gameObject.AddComponent<AudioSource>();
		exhaleSound.clip = exhaleSoundClip;
		exhaleSound.playOnAwake = false;
		
		beepSound = gameObject.AddComponent<AudioSource>();
		beepSound.clip = beepSoundClip;
		beepSound.loop = true;
		beepSound.playOnAwake = false;
		beepSound.volume = 0.01f;
		beepSound.Play();
		
		rocketSound = gameObject.AddComponent<AudioSource>();
		rocketSound.clip = rocketSoundClip;
		rocketSound.loop = true;
		
		secondaryRocketSound = gameObject.AddComponent<AudioSource>();
		secondaryRocketSound.clip = secondaryRocketSoundClip;
		secondaryRocketSound.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (Input.GetKeyDown(KeyCode.W)) {
			W = true;
		}
		if (Input.GetKeyUp(KeyCode.W)) {
			W = false;
		}
		
		if (Input.GetKeyDown(KeyCode.A)) {
			A = true;
		}
		if (Input.GetKeyUp(KeyCode.A)) {
			A = false;
		}
		
		if (Input.GetKeyDown(KeyCode.S)) {
			S = true;
		}
		if (Input.GetKeyUp(KeyCode.S)) {
			S = false;
		}
		
		if (Input.GetKeyDown(KeyCode.D)) {
			D = true;
		}
		if (Input.GetKeyUp(KeyCode.D)) {
			D = false;
		}
		

		if (Input.GetKeyDown(KeyCode.F)) {
			F = true;
		}
		if (Input.GetKeyUp(KeyCode.F)) {
			F = false;
		}
		
		if (Input.GetKeyDown(KeyCode.L)) {
			L = true;
		}
		if (Input.GetKeyUp(KeyCode.L)) {
			L = false;
		}
		
		
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			UP = true;
		}
		if (Input.GetKeyUp(KeyCode.UpArrow)) {
			UP = false;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			DOWN = true;
		}
		if (Input.GetKeyUp(KeyCode.DownArrow)) {
			DOWN = false;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			LEFT = true;
			LEFTCounter += 1;
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow)) {
			LEFT = false;
			LEFTCounter = 0;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			RIGHT = true;
			RIGHTCounter += 1;
		}
		if (Input.GetKeyUp(KeyCode.RightArrow)) {
			RIGHT = false;
			RIGHTCounter = 0;
		}
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			Space = true;
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			Space = false;
		}
		if (Input.GetKeyDown(KeyCode.X)) {
			X = true;
		}
		if (Input.GetKeyUp(KeyCode.X)) {
			X = false;
		}
		
		//////////////////
		/// reset values
		jumping = false;
		
		
		
		if (!A && !D) {
			walking = false;
			speed = 0f;
		}
		
		
		
		rb.drag = 0;
		
		if (ch.isAlive()) {
			rocketFuel -= 1 * Time.deltaTime;
		}
		
		if (!inContact) {
			grounded = false;
		}
		
		if (grounded) {
			falling = false;
			rb.drag = 2;
			rocketSound.Pause();
			secondaryRocketSound.Pause();
			primaryRocket = false;
			secondaryRocket = false;
			
			
			if (!crouching && ch.isAlive()) {
				if (D) {
					direction = "Right";
					walking = true;
					speed = speedX;
					rb.AddForce(new Vector2(4000f, 0));
//					rb.AddForce(new Vector2(0, -1000f));
					
					// if moving,
					// cancel normal breathing
//					if (normalBreathing) {
//						idleBreathingSound.Pause();
//					}
//					normalBreathing = false;
				}
				
				if (A) {
					direction = "Left";
					walking = true;
					speed = -speedX;
					rb.AddForce(new Vector2(-4000f, 0));
//					rb.AddForce(new Vector2(0, -1000f));
					
					// if moving,
					// cancel normal breathing
//					if (normalBreathing) {
//						idleBreathingSound.Pause();
//					}
//					normalBreathing = false;
				}
				
				
				
			} else if (Input.GetKeyUp(KeyCode.W) && ch.isAlive()) {
				jump();
			}
			
			
		} else {
			// if not grounded
			if (rb.velocity.y < 1) {
				if (!falling) {
					falling = true;
					fallTrigger = true;
				} else {
					fallTrigger = false;
				}
				
			}
			if (ch.alive && rocketFuel > 0) {
				if (Input.GetKeyDown(KeyCode.W)) {
					rocketSound.Play();
					primaryRocket = true;
					anim.SetTrigger("Rocket");
					
				}
				
				if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) {
					secondaryRocketSound.Play();
					secondaryRocket = true;
					anim.SetTrigger("Rocket");
				}
				
				
				if (!W) {
					rocketSound.Pause();
					primaryRocket = false;
				}
				
				if (!(A || D)) {
					secondaryRocketSound.Pause();
					secondaryRocket = false;
				}
				
				if (W) {
					rb.AddForce(new Vector2(0, 2000f));
				}
				
				if (A && secondaryRocket) {
					rb.AddForce(new Vector2(-1000f, 0));
					direction = "Left";
				}
				
				if (D && secondaryRocket) {
					rb.AddForce(new Vector2(1000f, 0));
					direction = "Right";
				}
			}
			
			if (!ch.alive) {
				rocketSound.Pause();
				secondaryRocketSound.Pause();
			}
		}
		
		
		
		if (W && grounded) {
			crouching = true;
			
			// cancel normal breathing
			if (normalBreathing) {
				idleBreathingSound.Pause();
			}
			normalBreathing = false;
		} else {
			crouching = false;
		}
		
		if (!walking && !crouching && !normalBreathing && ch.isAlive()) {
			normalBreathing = true;
			idleBreathingSound.volume = 0.3f;
			idleBreathingSound.Play();

		}
		
//		prevTransform = new Transform(transform);
		if (primaryRocket) {
			rocketFuel -= 60 * Time.deltaTime;
		}
		
		if (secondaryRocket) {
			rocketFuel -= 30 * Time.deltaTime;
		}
		
		if (rocketFuel <= 0) {
			rocketFuel = 0;
			ch.health = -1;
		}
		
		if (rocketFuel > maxRocketFuel) {
			rocketFuel = maxRocketFuel;
		}
		
		weaponsHandling();
		
		
		tb.offsetMax = new Vector2((maxRocketFuel - rocketFuel) * -0.4f, 12);
		
		if (Space) {
//			ch.heal(1);
		}
		
		inContact = false;
	}


	
	void movePlayer() {
		// rb.velocity = new Vector3(speed, rb.velocity.y, 0);
		
	}
	
	void jump() {
		if (ch.isAlive()) {
			// rb.velocity = (new Vector2(rb.velocity.x, jumpSpeedY));
			rb.AddForce(new Vector2(0, jumpSpeedY * 50 * 80));
			// grounded = false;
			jumping = true;
			
			exhaleSound.volume = 0.07f;
			exhaleSound.Play();
			removeCollisions();
		}
		
		
		
	}
	

	
	void OnCollisionEnter2D(Collision2D other) {
		float diffVel = (float) Math.Pow(other.relativeVelocity.magnitude/4, 2);
		
		if (diffVel > 0.7 && !walking) {
			collisionSound.volume = diffVel * 2;
			collisionSound.pitch = UnityEngine.Random.Range(0.6f, 0.8f);
			collisionSound.Play();
		}
		
		
		if (other.gameObject.tag == "GROUND" || other.gameObject.tag == "PICKUP") {
			groundCollisions.Add(other.gameObject);
			
			
			
//			foreach (GameObject o in groundCollisions) {
//				print(o);
//			}
			
			
			
		}
		
		if (other.gameObject.tag == "Spikes") {
			Spikes s = other.gameObject.GetComponent<Spikes>();
			ch.damage(s.damage);
		}
	}
	
	void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "GROUND" ||
			other.gameObject.tag == "GROUND2" ||
			other.gameObject.tag == "PICKUP" ||
			other.gameObject.tag == "Spikes" ||
			other.gameObject.tag == "BossEnemy") {
			
			inContact = true;
			
			// if not already grounded
			if (!grounded) {
				grounded = true;
				

			}
			
			
		}

		
	}
	
	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag == "GROUND" || other.gameObject.tag == "PICKUP" || other.gameObject.tag == "Spikes") {
			// grounded = false;
			groundCollisions.Remove(other.gameObject);
			
			if (groundCollisions.Count < 1) {
				grounded = false;
			}
			
//			foreach (GameObject o in groundCollisions) {
//				print(o);
//			}

		}
	}
	
	void removeCollisions() {
		groundCollisions.Clear();
	}

	void OnTriggerStay2D(Collider2D other) {
		
		if (other.gameObject.tag == "PICKUP") {
			PickupUpdate pu = other.gameObject.GetComponent<PickupUpdate>();
			pickupPanel.SetActive(true);
			pickupText.enabled = true;
			pickupText.text = "Press 'Spacebar' to pick up " + pu.weaponName + "";
			
			if (Space) {
				if (pu.remove) {
					
				
					activeWeaponName = pu.weaponName;
					// disable all of the other objects
					disableAllWeapons();
			
					// add the corresponding weapon to ownedWeapons
					GameObject w;
					if (weapons.TryGetValue(activeWeaponName, out w)) {
						if (!ownedWeapons.Contains(w)) {
							ownedWeapons.Add(w);
						}
						activeWeapon = w;
						weaponNameText.text = activeWeapon.GetComponent<GenericWeapon>().weaponName;
						
						// w.SetActive(true);
						w.GetComponent<Switch>().enableSwitch();
						weaponIndex = ownedWeapons.Count - 1;
						activeWeapon = ownedWeapons[weaponIndex];
					}
					
					
					// enable the 1 object
					
					pu.destroy();
					pickupPanel.SetActive(false);
					pickupText.enabled = false;
				} else {
					// you will actually take it
					
					activeWeaponName = pu.weaponName;
					
					
					// disable all weapons
					disableAllWeapons();
					
					// enable the parent gameobject
					GameObject w;
					if (weapons.TryGetValue(activeWeaponName, out w)) {
						// if the receptacle is not already "owned"
						if (!ownedWeapons.Contains(w)) {
							ownedWeapons.Add(w);
						}
						
						activeWeapon = w;
						weaponNameText.text = activeWeapon.GetComponent<GenericWeapon>().weaponName;
						
						// w.SetActive(true);
						w.GetComponent<Switch>().enableSwitch();
						weaponIndex = ownedWeapons.Count - 1;
						activeWeapon = ownedWeapons[weaponIndex];
					} else {
						
					}
					
					// add the parent gameobject to the owned weapons
					pu.destroy();
					pickupPanel.SetActive(false);
					pickupText.enabled = false;
				}
			}
			
			// rb.AddForce(new Vector2(0, jumpSpeedY * 50 * 80));
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "PICKUP") {
			// hide the pick up message
			pickupPanel.SetActive(false);
			pickupText.enabled = false;
		}
	}
	
	Boolean changed = false;
	
	void weaponsHandling() {
		changed = false;
		
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			weaponIndex += 1;
			changed = true;
		}
		
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			weaponIndex -= 1;
			changed = true;
		}
		
		if (weaponIndex < 0) {
			weaponIndex += ownedWeapons.Count;
		}
		
		if (weaponIndex + 1 > ownedWeapons.Count) {
			weaponIndex -= ownedWeapons.Count;
		}
		
		if (changed) {
			activeWeapon = ownedWeapons[weaponIndex];
			
			weaponNameText.text = activeWeapon.GetComponent<GenericWeapon>().weaponName;
			disableAllWeapons();
			enableWeapon(activeWeapon.GetComponent<GenericWeapon>().weaponName);
			
		}
		
		
		
	}
	
	void disableAllWeapons() {
		// disable all weapons
		foreach (KeyValuePair<string, GameObject> weapon in weapons) {
			// weapon.Value.SetActive(false);
			weapon.Value.GetComponent<Switch>().disableSwitch();
		}
	}
	
	void enableWeapon(string n) {
		GameObject w;
		
		// try to find hidden weapon
		if (weapons.TryGetValue(n, out w)) {
			activeWeapon = w;
			
			
			// reflect changes graphically
			// w.SetActive(true);
			w.GetComponent<Switch>().enableSwitch();
		}
	}
}
