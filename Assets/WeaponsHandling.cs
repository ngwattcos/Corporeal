using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsHandling : MonoBehaviour {
	
	CharacterUpdate cu;
	string weaponName;
	GameObject weaponNameText;

	// Use this for initialization
	void Start () {
		cu = GetComponent<CharacterUpdate>();
		weaponName = cu.activeWeapon.GetComponent<GenericWeapon>().weaponName;
		weaponNameText = GameObject.FindGameObjectWithTag("WeaponNameText");
//		weaponNameText.text.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		weaponName = cu.activeWeapon.GetComponent<GenericWeapon>().weaponName;
//		weaponNameText.text = "";
	}
}
