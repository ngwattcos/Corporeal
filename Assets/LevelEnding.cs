using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnding : MonoBehaviour {
	
	public SemiWeaponBehavior swb;
	public BlackHole bh;
	
	public GameObject uiManager;
	PauseGame ui;

	// Use this for initialization
	void Start () {
		ui = uiManager.GetComponent<PauseGame>();
		
//		swb = GameObject.FindGameObjectWithTag("Cathode Ray Tube").GetComponent<SemiWeaponBehavior>();
//		bh = GameObject.FindGameObjectWithTag("Black Hole").GetComponent<BlackHole>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			ui.endLevel(bh.totalCaptured, swb.wastedSpecimens, swb.rounds);
		}
		
	}
}
