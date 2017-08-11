using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveEmitterMars : MonoBehaviour {
	
	// Use this for initialization

	public GameObject passive1;
	public GameObject passive2;
	public GameObject passive3;

	public bool inRange = false;

	public int maxPassives = 20;
	public int passivesCounter = 0;

	public float spawnTime = 0.5f;
	float counter = 0f;

	GameObject[] passives;
	void Start () {
		passives = new GameObject[] {passive1, passive2, passive3};
	}

	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;

		if (counter > spawnTime) {
			counter -= spawnTime;
			if (passivesCounter < maxPassives && inRange) {
				//			if (inRange) {
				GameObject passive = Instantiate(
					passives[(int) Mathf.Round(Random.Range(0, passives.Length - 1))],
					new Vector2(Random.Range(transform.position.x - 1, transform.position.x + 1),
						Random.Range(transform.position.y - 1, transform.position.y + 4)),
					Quaternion.identity) as GameObject;


				passivesCounter += 1;
			}

		}

		inRange = false;
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			inRange = true;
		}
	}
}
