using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscreteAmmoWeapon : MonoBehaviour {
	
	public List<GameObject> ammo;
	
	// Use this for initialization
	void Start () {
		ammo = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void add(GameObject o) {
		ammo.Add(o);
	}
	
	public void remove(GameObject o) {
		ammo.Remove(o);
	}
	
	public void remove() {
		ammo.RemoveAt(ammo.Count - 1);
	}
}
