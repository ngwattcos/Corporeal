using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSpeaker : MonoBehaviour {

	DialogueController dc;
	
	public string sender = "";
	public string dialog = "";
	public float time = 2.0f;
	
	// Use this for initialization
	void Start () {
		dc = GameObject.FindGameObjectWithTag("Player").GetComponent<DialogueController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			dc.addDialogue(dialog, sender, time);
			Destroy(gameObject);
		}
	}
}
