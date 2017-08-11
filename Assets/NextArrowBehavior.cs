using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextArrowBehavior : MonoBehaviour {

	GameObject arrowSprite;
	Collider2D edge;
	
	// Use this for initialization
	void Start () {
		edge = GetComponent<Collider2D>();
		
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		
		foreach (Transform child in allChildren) {
			if (child.gameObject.tag == "NextArrowSprite") {
				arrowSprite = child.gameObject;
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {
//		Debug.Log("adasdf");
		if (other.gameObject.tag == "Player") {
			// player has progressed to this point
			Destroy(gameObject);
			
//			Debug.Log("xxxxx");
		}
	}
}
