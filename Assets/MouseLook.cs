using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
//using UnityEditor;

public class MouseLook : MonoBehaviour {
	
	public float sensitivityY = 15f;
	public float minimumZ = -60F;
	public float maximumZ = 60f;
	
	public Vector3 mousePos;
	
	float angleZ = 0f;
	Quaternion originalQuaternion;

	// Use this for initialization
	void Start () {
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		
		angleZ = Vector2.Angle(Vector2.right, mousePos - transform.position);
		
		angleZ = clampAngle(angleZ, minimumZ, maximumZ);
	}
	
	// Update is called once per frame
	void Update () {
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}   
	
	public static float clampAngle(float _angle, float min, float max) {
		if (_angle < -360f) {
			_angle += 360f;
		}
		
		if (_angle > 360) {
			_angle -= 360f;
		}
		
		return Mathf.Clamp(_angle, min, max);
	}
}
