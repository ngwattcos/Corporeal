using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExhaust : MonoBehaviour {
	
	public CharacterUpdate cu;
	
	public CharacterHealth ch;
	
	public GameObject exhaust;
	
	public GameObject exhaust0;
	public GameObject exhaust1;
	public GameObject exhaust2;
	public GameObject exhaust3;
	public GameObject exhaust4;
	public GameObject exhaust5;
	public GameObject exhaust6;

	GameObject[] exhausts;

	public float flickerTime;
	public float counter = 0;

	int exhaustInt = 0;

	// Use this for initialization
	void Start () {
		flickerTime = 0.06f;
		
		cu = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterUpdate>();
		ch = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterHealth>();
		
		exhaust = GameObject.FindGameObjectWithTag("Exhaust");
		
		exhaust0 = GameObject.FindGameObjectWithTag("exhaust0");
		exhaust1 = GameObject.FindGameObjectWithTag("exhaust1");
		exhaust2 = GameObject.FindGameObjectWithTag("exhaust2");
		exhaust3 = GameObject.FindGameObjectWithTag("exhaust3");
		exhaust4 = GameObject.FindGameObjectWithTag("exhaust4");
		exhaust5 = GameObject.FindGameObjectWithTag("exhaust5");
		exhaust6 = GameObject.FindGameObjectWithTag("exhaust6");

		exhaust0.SetActive(false);
		exhaust1.SetActive(false);
		exhaust2.SetActive(false);
		exhaust3.SetActive(false);
		exhaust4.SetActive(false);
		exhaust5.SetActive(false);

		exhausts = new GameObject[] {exhaust0, exhaust2, exhaust3, exhaust4, exhaust5, exhaust6};

		
	}

	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;

		if (ch.alive && counter > flickerTime) {
			counter -= flickerTime;

			for (int i = 0; i < exhausts.Length; i++) {
				exhausts[i].SetActive(false);
			}

			exhaustInt += 1;
			if (exhaustInt > exhausts.Length - 1) {
				exhaustInt -= (exhausts.Length - 1);
			}

			exhausts[exhaustInt].SetActive(true);
		}
		
		if ((cu.secondaryRocket || cu.primaryRocket) && ch.alive) {
			exhaust.SetActive(true);
		} else {
			exhaust.SetActive(false);
		}
	}
}
