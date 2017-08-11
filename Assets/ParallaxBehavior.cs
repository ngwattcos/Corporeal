using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBehavior : MonoBehaviour {
	
	public float parallaxSpeedX;
	public float parallaxSpeedY;
	float lastCameraX;
	float lastCameraY;
	Transform cameraTransform;

	// Use this for initialization
	void Start () {
		cameraTransform = Camera.main.transform;
	}
	
	// Update is called once per frame
	void LateUpdate() {

		float deltaX = cameraTransform.position.x * 1.0f - lastCameraX * 1.0f;
		float deltaY = cameraTransform.position.y * 1.0f - lastCameraY * 1.0f;
		transform.position += Vector3.right * deltaX * parallaxSpeedX;
		transform.position += Vector3.up * deltaY * parallaxSpeedY;

		lastCameraX = cameraTransform.position.x * 1.0f;
		lastCameraY = cameraTransform.position.y * 1.0f;
		

	}
	
}
