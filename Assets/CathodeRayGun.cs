using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CathodeRayGun : MonoBehaviour {
	public GameObject ray1;
	public GameObject ray2;
	public GameObject ray3;
	public GameObject ray4;
	
	GameObject[] rays;
	
	public float flickerTime;
	public float counter = 0;
	
	int rayInt = 0;
	
	Switch sw;

	// Use this for initialization
	void Start () {
		sw = GetComponent<Switch>();
		
		ray1 = GameObject.FindGameObjectWithTag("ray1");
		ray2 = GameObject.FindGameObjectWithTag("ray2");
		ray3 = GameObject.FindGameObjectWithTag("ray3");
		ray4 = GameObject.FindGameObjectWithTag("ray4");
		
		ray2.SetActive(false);
		ray3.SetActive(false);
		ray4.SetActive(false);
		
		rays = new GameObject[] {ray1, ray2, ray3, ray4};
		
		flickerTime = Random.Range(0.5f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (sw.getEnabled()) {
			counter += Time.deltaTime;
			
			if (counter > flickerTime) {
				counter = 0;
				flickerTime = 0.1f;
				
				
				
				for (int i = 0; i < rays.Length; i++) {
					rays[i].SetActive(false);
				}
				
				rayInt += 1;
				if (rayInt > rays.Length - 1) {
					rayInt -= (rays.Length - 1);
				}
				
				rays[rayInt].SetActive(true);
			}
		}
	}
}
