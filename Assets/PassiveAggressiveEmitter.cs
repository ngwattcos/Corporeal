using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveAggressiveEmitter : MonoBehaviour {

	// Use this for initialization

	public GameObject passive1;
	public GameObject passive2;
	public GameObject passive3;
	
	public EnemyUpdate eu;
	GameObject player;

	public float velocityX = 0f;
	public float velocityY = 0f;

	float spawnTime = 6f;
	float counter = 0f;
	
	public int numPassivesPerSpawn = 5;
	
	bool farRange = false;
	

	GameObject[] passives;
	void Start () {
		passives = new GameObject[] {passive1, passive2, passive3};
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;
		
		if (eu.seenPlayer && (eu.transform.position - player.transform.position).sqrMagnitude > 25f) {
			farRange = true;
		} else {
			farRange = false;
		}
		
		if (farRange) {
			counter += Time.deltaTime;
			if (counter > spawnTime) {
				counter = 0f;
			}
			
			if (counter == 0f) {
				for (int i = 0; i < 2; i++) {
					GameObject passive = Instantiate(
						passives[(int) Mathf.Round(Random.Range(0, passives.Length - 1))],
						new Vector2(transform.position.x, transform.position.y),
						Quaternion.identity) as GameObject;

					if (eu.direction == "Left") {
						passive.GetComponent<Rigidbody2D>().velocity = new Vector2(-velocityX, velocityY);
					} else {
						passive.GetComponent<Rigidbody2D>().velocity = new Vector2(velocityX, velocityY);
					}
				}
			}
			
			
		} else {
			counter = 0;
		}

	}

	void OnTriggerStay2D(Collider2D other) {
		
	}
}
