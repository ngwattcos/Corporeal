using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using UnityEngine.UI;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

public class QSDBehavior : MonoBehaviour {
	
//	public GameObject center;
	
	public bool inHands = false;
	
	public bool turnedOn = false;
	public GameObject light;
	
	public Switch sw;
	
	Collider2D sensor;
	
	public Switch turnedOnSwitch;
	
	public AudioSource turnOnSound;
	public AudioClip turnOnClip;
	
	Text offText;

	// Use this for initialization
	void Start () {
		light.SetActive(turnedOn);
		
		turnedOnSwitch = gameObject.AddComponent<Switch>();
		turnedOnSwitch.disableSwitch();
		
		sensor = GetComponent<Collider2D>();
		turnOnSound = gameObject.AddComponent<AudioSource>();
		turnOnSound.clip = turnOnClip;
		turnOnSound.loop = false;
		
		offText = GameObject.FindGameObjectWithTag("QSDTag").GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log("adfdf: " + transform.parent.transform.parent.gameObject.tag);
//		sensor.enabled = sw.getEnabled();
		
		if (inHands && sw.getEnabled()) {
			if (Input.GetButtonDown("Fire1")) {
//				turnedOn = !turnedOn;
				turnedOnSwitch.setSwitch(!turnedOnSwitch.getEnabled());
			}
			light.SetActive(turnedOnSwitch.getEnabled());
		}
		
		if (turnedOnSwitch.wasEnabled()) {
			turnOnSound.Play();
		}
		
		

		offText.enabled = inHands && !turnedOnSwitch.getEnabled();
	
	}
	
	
	void OnTriggerStay2D(Collider2D other) {
//		GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterHealth>().damage(0.01f);
		if (turnedOnSwitch.getEnabled()) {
			if (other.gameObject.GetComponent<QuantumBehavior>() != null) {
				QuantumBehavior qb = other.gameObject.GetComponent<QuantumBehavior>();
				qb.scanned = true;
			}

			if (other.gameObject.GetComponent<QuantumPassiveBehavior>() != null) {
				QuantumPassiveBehavior qb = other.gameObject.GetComponent<QuantumPassiveBehavior>();
				qb.scanned = true;
			}
		}

		
	}
}
