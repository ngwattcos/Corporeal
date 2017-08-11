using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumPassiveBehavior : MonoBehaviour {
	
	public bool scanned = false;
	public bool alreadyScanned = false;

	SpriteRenderer sr;
	
	GameObject canvas;

	//	public GameObject qsd;
	
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		
		canvas = transform.Find("Canvas").gameObject;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		if (scanned) {
			enableChildren();
			canvas.SetActive(true);
		} else {
			disableChildren();
			canvas.SetActive(false);
		}

		scanned = false;
		alreadyScanned = false;
	}
	
	void OnTriggerStay2D(Collider2D other) {

	}
	
	void enableChildren() {
		sr.color = new Color(1f, 1f, 1f, 0.8f);
	}
	
	void disableChildren() {
		sr.color = new Color(1f, 1f, 1f, 0.1f);
	}
}
