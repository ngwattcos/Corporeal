using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;

public class QuantumBehavior : MonoBehaviour {
	
//	SpriteRenderer sr;
	
	public bool scanned = false;
	public bool alreadyScanned = false;
	
	List<SpriteRenderer> spriteRenderers;
	
	GameObject canvas;
	
//	public GameObject qsd;
	QSDBehavior qb;

	// Use this for initialization
	void Start () {
//		sr = GetComponent<SpriteRenderer>();
		spriteRenderers = new List<SpriteRenderer>();
		
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		
		foreach (Transform child in allChildren) {
			if (child.gameObject.GetComponent<SpriteRenderer>() != null) {
				Debug.Log("Added child.");
				spriteRenderers.Add(child.gameObject.GetComponent<SpriteRenderer>());
			}
		}
		
//		GameObject qsd = GameObject.FindGameObjectWithTag("Scanner");
		qb = GameObject.FindGameObjectWithTag("Scanner").GetComponent<QSDBehavior>();
		
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
		if (other.gameObject.tag == "Player") {
//			scanned = true;
		}
		
	}

	void enableChildren() {
		foreach (SpriteRenderer sr in spriteRenderers) {
			sr.color = new Color(1f, 1f, 1f, 0.8f);
		}
	}
	
	void disableChildren() {
		foreach (SpriteRenderer sr in spriteRenderers) {
			sr.color = new Color(1f, 1f, 1f, 0.1f);
		}
	}
}
