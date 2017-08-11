using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassivePlantUpdate : MonoBehaviour {

	Rigidbody2D rb;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		Physics2D.IgnoreLayerCollision(9, 13);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
