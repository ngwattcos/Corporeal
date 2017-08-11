using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGraphics : MonoBehaviour {
	
	Switch sw;
	SpriteRenderer gun;
	SpriteRenderer[] srChildren;
	Light[] lChildren;

	// Use this for initialization
	void Start () {
		sw = GetComponent<Switch>();
		
		// may or may not be null
		gun = GetComponent<SpriteRenderer>();
		
		srChildren = GetComponentsInChildren<SpriteRenderer>(true);
		lChildren = GetComponentsInChildren<Light>(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (sw.getEnabled()) {
			showGun();
			showChildren();
		} else {
			hideGun();
			hideChildren();
		}
	}
	
	public void updateReferences() {

		// may or may not be null
		gun = GetComponent<SpriteRenderer>();

		srChildren = GetComponentsInChildren<SpriteRenderer>(true);
		lChildren = GetComponentsInChildren<Light>(true);
	}
	
	void showGun() {
		if (gun != null) {
			gun.enabled = true;
		}
		
	}
	
	void hideGun() {
		if (gun != null) {
			gun.enabled = true;
		}

	}
	
	void showChildren() {
		foreach (SpriteRenderer sr in srChildren) {
			sr.enabled = true;
		}
		
		foreach (Light l in lChildren) {
			l.enabled = true;
		}
	}
	
	void hideChildren() {
		foreach (SpriteRenderer sr in srChildren) {
			sr.enabled = false;
		}

		foreach (Light l in lChildren) {
			l.enabled = false;
		}
	}
}
