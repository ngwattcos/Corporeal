using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeadLampBehavoir : MonoBehaviour {
	
	CharacterUpdate cu;
	public CharacterHealth ch;
	GameObject headLamp;
	Boolean active = true;
	
	// Use this for initialization
	void Start () {
//		ch = GameObject.transform.root.gameObject.GetComponent<CharacterHealth>();
//		cu = GameObject.transform.root.gameObject.GetComponent<CharacterUpdate>();
		headLamp = GameObject.FindGameObjectWithTag("HeadLamp");
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown(KeyCode.L) && ch.isAlive()) {
//			active = !active;
//			headLamp.SetActive(active);
//		}
	}
}
