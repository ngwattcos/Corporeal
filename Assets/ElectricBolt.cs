using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBolt : MonoBehaviour {

	public List<GameObject> flashes;
	
	int flashIdx = 0;
	
	float flashTimer = 0;
	float flashTime = 0.10f;
	
	// Use this for initialization
	void Start () {
		flashes = new List<GameObject>();
		
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		
		foreach (Transform o in allChildren) {
			if (o.gameObject.tag == "Ray") {
				flashes.Add(o.gameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		
		flashTimer += Time.deltaTime;
		
		if (flashTimer > flashTime) {
			flashTimer -= flashTime;
			flashIdx += 1;
			
			if (flashIdx >= flashes.Count) {
				flashIdx -= flashes.Count;
			}
			
			for (int i = 0; i < flashes.Count; i++) {
				flashes[i].SetActive(false);
			}
			
			flashes[flashIdx].SetActive(true);
		}
		
		
		
		
	}
}
