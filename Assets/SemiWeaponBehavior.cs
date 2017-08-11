using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

public class SemiWeaponBehavior : MonoBehaviour {
	
	CharacterUpdate cu;
	
	public Transform alignment;
	
	public int rounds = 10;
	
	public int wastedSpecimens = 0;
	
	
	public float reloadTime = 0.2f;
	
	public float reloadCounter = 0;
	
	public float projectileSpeed = 20f;
	
	public BlackHole bh;
	
	Text roundsLabel;
	Text roundsText;
	
	public GameObject player;
	
	public GameObject bulletPrefab;
	public Transform spawnLoc;
	
	Switch sw;
	
	// Use this for initialization
	void Start () {
		
		player = GameObject.FindGameObjectWithTag("Player");
		
		sw = GetComponent<Switch>();
		
		cu = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterUpdate>();
		alignment = GameObject.FindGameObjectWithTag("AlignmentAxis").GetComponent<Transform>();
		
		roundsLabel = GameObject.FindGameObjectWithTag("RoundsLabel").GetComponent<Text>();
		roundsText = GameObject.FindGameObjectWithTag("Rounds").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		rounds = bh.capturedPassives;
		reloadCounter += Time.deltaTime;
		
		if (reloadCounter >= reloadTime && rounds > 0) {
			if (Input.GetButton("Fire1") && sw.getEnabled()) {
				fire();
				
				reloadCounter = 0;
			}
		}
		
		
		roundsLabel.enabled = sw.getEnabled();
		roundsText.enabled = sw.getEnabled();
		
		if (sw.getEnabled()) {
			roundsText.text = "" + rounds + "";
		}
	}
	
	void fire() {
		bh.capturedPassives -= 1;
		wastedSpecimens += 1;
		GameObject bullet = Instantiate(bulletPrefab, spawnLoc.position, spawnLoc.rotation) as GameObject;
		Debug.Log(spawnLoc.eulerAngles.z);
		
		if (player.GetComponent<CharacterUpdate>().direction == "Right") {
			bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Cos(spawnLoc.eulerAngles.z * 3.1415f/180f), Mathf.Sin(spawnLoc.eulerAngles.z * 3.1415f/180f), 0) * projectileSpeed;
		} else {
			bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(-Mathf.Cos(spawnLoc.eulerAngles.z * 3.1415f/180f), -Mathf.Sin(spawnLoc.eulerAngles.z * 3.1415f/180f), 0) * projectileSpeed;
			bullet.transform.localScale = new Vector3(-1, 1, 1);
		}
	}
	
	public void addRounds(int _rounds) {
		rounds += _rounds;
	}
}
