using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveUpdate : MonoBehaviour {
	
	public float life = 60f;
	public float lifeCounter = 0f;
	
	Rigidbody2D rb;
	
	public float forceX = 0;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		lifeCounter += Time.deltaTime;
		rb.AddForce(new Vector3(forceX, 0, 0));
		
		rb.AddForce(new Vector2(0, Random.Range(-5f, 5f)));
		
		
		if (lifeCounter > life) {
			Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
//			other.gameObject.GetComponent<CharacterHealth>().damage(1);
		}
	}
}
