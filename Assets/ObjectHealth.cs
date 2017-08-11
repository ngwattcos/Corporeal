using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ObjectHealth : MonoBehaviour {

	
	public float[] health;
	public float physicalHealth;
	public float electricHealth;
	public float magneticHealth;
	public float matterHealth;
	
	
	public float[] maxHealth;
	public float maxPhysicalHealth;
	public float maxElectricHealth;
	public float maxMagneticHealth;
	public float maxMatterHealth;
	
	public float[] sensitivities;
	public float physicalSensitivity;
	public float electricalSensitivity;
	public float magneticSensitivity;
	public float matterSensitivity;
	
	public Boolean alive = true;
	
	// Use this for initialization
	void Start () {
		health = new float[] {physicalHealth, electricHealth, magneticHealth, matterHealth};
		maxHealth = new float[] {maxPhysicalHealth, maxElectricHealth, maxMagneticHealth, maxMatterHealth};
		sensitivities = new float[] {physicalSensitivity, electricalSensitivity, magneticSensitivity, matterSensitivity};
	}
	
	// Update is called once per frame
	void Update () {
		Boolean allHealth = true;
		
		for (int i = 0; i < health.Length; i++) {
			if (health[i] > maxHealth[i]) {
				health[i] = maxHealth[i];
			}

			// If any of the "healths" are 0, then it is dead
			if (alive && health[i] <= 0) {
				alive = false;
			}

			if (health[i] <= 0) {
				allHealth = false;
			}
		}
		
		if (allHealth) {
			alive = true;
		}
		
		
		if (!isAlive()) {
			Destroy(gameObject);
		}
	}
	
	public void damage(float[] damage) {
		for (int i = 0; i < health.Length; i++) {
			health[i] -= damage[i];
		}
	}

	public void heal(float[] amount) {
		for (int i = 0; i < health.Length; i++) {
			health[i] += amount[i];
		}
	}
	
	public void restoreHealth() {
		for (int i = 0; i < health.Length; i++) {
			health[i] = maxHealth[i];
		}
	}

	public bool isAlive() {
		Boolean stillAlive = true;
		
		for (int i = 0; i < health.Length; i++) {
			if (health[i] <= 0) {
				stillAlive = false;
			}
		}
		
		return stillAlive;
	}
}
