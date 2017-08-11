using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class EnemyHealth : MonoBehaviour {
	
	public float maxHealth;
	public float health;
	public float electricHealth;
	
	public float healthFactor;
	public float electricFactor;
	
	float minHealth;
	
	public bool alive;
	public RectTransform hb;
	
	public bool isBoss = true;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (health > maxHealth) {
			health = maxHealth;
		}
		if (electricHealth > maxHealth) {
			electricHealth = maxHealth;
		}
		if (health < 0) {
			health = 0;
		}
		if (electricHealth < 0) {
			electricHealth = 0;
		}
		
		alive = health > 0 && electricHealth > 0;
		
		if (isBoss) {
			minHealth = Mathf.Min(health/maxHealth, electricHealth/maxHealth);
			hb.offsetMax = new Vector2((1f - minHealth) * -5f, 0);
		} else {
			if (!alive) {
				death();
			}
		}
		
		
	}
	
	public void damage(float _healthAmt, float _electricAmt) {
		health -= healthFactor * _healthAmt;
		electricHealth -= _electricAmt;
	}
	
	
	public void death() {
		Destroy(gameObject);
		
		if (GetComponent<DeathTrigger>() != null) {
			DeathTrigger dt = GetComponent<DeathTrigger>();
			dt.deathTrigger();
		}
	}
	
	public void deathRemove() {
		
	}
	
	public void beKilled() {
		damage(maxHealth, maxHealth);
	}
}
