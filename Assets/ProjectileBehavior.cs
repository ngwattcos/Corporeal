using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour {
	
	public float damage;
	public float electricDamage;
	
	public float life = 10f;
	public float timer = 0f;
	

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision(11, 12);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		
		if (timer > life) {
			Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "BossEnemy") {
			other.gameObject.GetComponent<EnemyHealth>().damage(damage, electricDamage);
			
		}
		
		if (other.gameObject.tag != "Player" && other.gameObject.tag != "Projectiles") {
			Destroy(gameObject);
		}
		
		
	}
}
