using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
	
	private bool prevEnabled = true;
	private bool isThisEnabled = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		prevEnabled = isThisEnabled;
	}
	
	public void enableSwitch() {
		prevEnabled = isThisEnabled;
		isThisEnabled = true;
	}
	
	public void disableSwitch() {
		prevEnabled = isThisEnabled;
		isThisEnabled = false;
	}
	
	public void setSwitch(bool _enabled) {
		isThisEnabled = _enabled;
	}
	
	public bool getEnabled() {
		return isThisEnabled;
	}
	
	public bool wasChanged() {
		return prevEnabled != isThisEnabled;
	}
	
	public bool wasEnabled() {
		return wasChanged() && isThisEnabled && !prevEnabled;
	}
	
	public bool wasDisabled() {
		return wasChanged() && !isThisEnabled;
	}
}
