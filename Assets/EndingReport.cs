using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingReport : MonoBehaviour {
	
	// for rounds fired
	
	public Text captured;
	public Text utilized;
	public Text remaining;
	
	public int numCaptured;
	public int numUtilized;
	public int numRemaining;
	
	public Image star1;
	public Image star2;
	public Image star3;
	public Image star4;
	public Image star5;

	// Use this for initialization
	void Start () {
//		swb = GameObject.FindGameObjectWithTag("Cathode Ray Tube").GetComponent<SemiWeaponBehavior>();
//		bh = GameObject.FindGameObjectWithTag("Black Hole").GetComponent<BlackHole>();
	}
	
	// Update is called once per frame
	void Update () {
		captured.text = "" + numCaptured;
		utilized.text = "" + numUtilized;
		remaining.text = "" + numRemaining;
		
		float grade = ((float) numRemaining) * 1.1f / ((float) numCaptured);
		
		star1.enabled = grade > 0.2f;
		star2.enabled = grade > 0.3f;
		star3.enabled = grade > 0.5f;
		star4.enabled = grade > 0.7f;
		star5.enabled = grade > 0.9f;
	}
}
