using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnemyUpdate : MonoBehaviour {
	
	public Transform target;
	Rigidbody2D rb;
	
	public bool onSlope = false;
	public bool inContact = false;
	public bool grounded = false;
	public float jumpSpeedY = 100f;
	
	public bool walking = false;
	public bool jumping = false;
	public bool falling = false;
	public bool attacking = false;
	public bool deathTrigger = false;
	public bool deathTriggered = false;
	public float walkStrength = 1000f;
	
	public float thrustForce = 500000f;
	public float rotationZ;
	public bool seenPlayer = false;
	
	public String direction;
	
	public List<GameObject> groundCollisions;
	
	public EnemyHealth eh;
	
	BoxCollider2D trigger;
	

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;
		eh = GetComponent<EnemyHealth>();
		rb = GetComponent<Rigidbody2D>();
		
		direction = "Left";
		
		trigger = gameObject.AddComponent<BoxCollider2D>();
		trigger.isTrigger = true;
		trigger.size = new Vector2(30f, 10f);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		walking = false;
		rb.drag = 0;
		attacking = false;
		falling = false;
		
		if (onSlope) {
			
		}
		
		if (inContact) {
			grounded = true;
		}
		
		if (seenPlayer) {
			trigger.enabled = false;
		} else {
			trigger.enabled = true;
		}
		
		if (grounded && eh.alive) {
			falling = false;
			rb.drag = 1;
			rb.gravityScale = 1f;
			
			
			if (onSlope) {
				if (rb.velocity.y > 0) {
					rb.gravityScale = 0.1f;
				}
				
			}
			
			if (seenPlayer) {
				
				if (transform.position.x > target.position.x) {
					direction = "Left";
				}

				if (transform.position.x < target.position.x) {
					direction = "Right";
				}
				if (Vector3.Distance((Vector3) (transform.position), (Vector3) target.position) > 3f) {
					if (transform.position.x > target.position.x) {
						moveLeft();
					}

					if (transform.position.x < target.position.x) {
						moveRight();
					}
				} else {
					attacking = true;
					walking = false;
				}
			}
			
			
		} else {
			if (rb.velocity.y < 0) {
				if (!falling) {
					falling = true;
				} else {

				}

			}
		}
		
		if (!eh.alive && !deathTriggered) {
			deathTrigger = true;
			deathTriggered = true;
		} else {
			deathTrigger = false;
		}
		
		
		onSlope = false;
		inContact = false;
	}
	
	void jump() {
//		if (ch.isAlive()) {
			// rb.velocity = (new Vector2(rb.velocity.x, jumpSpeedY));
			rb.AddForce(new Vector2(0, jumpSpeedY * 50 * 80));
			jumping = true;

			
			removeCollisions();
//		}



	}
	
	void moveLeft() {
		direction = "Left";
		walking = true;
		rb.AddForce(new Vector2(-walkStrength, 0));
	}
	
	void moveRight() {
		direction = "Right";
		walking = true;
		rb.AddForce(new Vector2(walkStrength, 0));
	}



	void OnCollisionEnter2D(Collision2D other) {
		float diffVel = (float) Math.Pow(other.relativeVelocity.magnitude/4, 2);

		if (diffVel > 0.7 && !walking) {
//			collisionSound.volume = diffVel * 2;
//			collisionSound.pitch = UnityEngine.Random.Range(0.6f, 0.8f);
//			collisionSound.Play();
		}


		if (other.gameObject.tag == "GROUND" || other.gameObject.tag == "PICKUP") {
//			groundCollisions.Add(other.gameObject);




		}

		if (other.gameObject.tag == "Player") {
//			eh.damage(10f, 10f);
		}
	}

	void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "GROUND" ||
			other.gameObject.tag == "GROUND2" ||
			other.gameObject.tag == "PICKUP" ||
			other.gameObject.tag == "Spikes" ||
			other.gameObject.tag == "Player") {

			inContact = true;
			grounded = true;

			// if not already grounded
			if (!grounded) {
				grounded = true;


			}
			
			if (other.gameObject.tag == "GROUND2") {
				onSlope = true;
			}


		}


	}

	void OnCollisionExit2D(Collision2D other) {
		
	}

	void removeCollisions() {
		groundCollisions.Clear();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
//			Destroy(gameObject);
			seenPlayer = true;
		}
	}

	void OnTriggerStay2D(Collider2D other) {

	}

	void OnTriggerExit2D(Collider2D other) {
		
	}
	
	void FixedUpdate() {
//		transform.rotation.y = Mathf.Clamp(transform.rotation.y, -25.0f, 25.0f);
//		rotationZ = transform.rotation.z;
//		rotationZ = Mathf.Clamp(rotationZ, -25f, 25f);
		
//		transform.rotation = Quaternion.Euler(new Vector3(rotationZ, 0, 0));
//		transform.rotation = Quaternion.AngleAxis(rotationZ, new Vector3(0, 0, 1));
		
//		if (transform.rotation.z > 30f) {
//			transform.localEulerAngles = new Vector3(30f, 0, 0);
//		}
//		
//		if (transform.rotation.z < -30f) {
//			transform.localEulerAngles = new Vector3(-30f, 0, 0);
//		}
	}
	
	public void moveFoward() {
		if (direction.Equals("Left")) {
			rb.AddForce(new Vector2(-thrustForce, 0));
		}
		if (direction.Equals("Right")) {
			rb.AddForce(new Vector2(thrustForce, 0));
		}
	}
}
