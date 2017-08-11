using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickupUpdate : MonoBehaviour {

	public string weaponName;
	
	public GameObject parent;
	
	public Boolean remove;
	
	public Rigidbody2D rb;
	
	public CircleCollider2D col;
	
	public CircleCollider2D trigger;
	
	public GameObject item;
	
	
	public float scale = 1.0f;
	

	// Use this for initialization
	void Start () {
		
		rb = GetComponent<Rigidbody2D>();
		
		col = gameObject.AddComponent<CircleCollider2D>();
		col.radius = 1f/4f * scale;
		trigger = gameObject.AddComponent<CircleCollider2D>();
		trigger.radius = 7f * scale;
		trigger.isTrigger = true;
		trigger.enabled = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void destroy() {
		if (remove) {
			Destroy(gameObject);
		} else {
			addToParent();
			disableComponents();
		}
	}
	
	public void addToParent() {
		gameObject.transform.SetParent(parent.transform);
		gameObject.transform.localPosition = new Vector2(0, 0);
		
//		parent.GetComponent<DiscreteAmmoWeapon>().add(gameObject);
		
		parent.GetComponent<WeaponGraphics>().updateReferences();
		if (item != null) {
			item.GetComponent<QSDBehavior>().inHands = false;
		}
	}
	
	public void disableComponents() {
		rb.simulated = false;
		col.enabled = false;
		trigger.enabled = false;
		
		if (item != null) {
			item.GetComponent<QSDBehavior>().inHands = true;
		}
	}
	
	public void enableComponents() {
		rb.simulated = true;
		col.enabled = true;
		trigger.enabled = true;
		enabled = true;
	}
}
