using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml.Linq;

public class ScrollingBackground : MonoBehaviour {
	
	public float backgroundSize;
	public float parallaxSpeedX;
	public float parallaxSpeedY;
	float offsetY;
	float lastCameraX;
	float lastCameraY;
	Transform cameraTransform;
	Transform[] layers;
	float viewZone = 1;
	int leftIndex;
	int rightIndex;

	// Use this for initialization
	void Start () {
		cameraTransform = Camera.main.transform;
		layers = new Transform[transform.childCount];
		
		for (int i = 0; i < transform.childCount; i++) {
			layers[i] = transform.GetChild(i);
		}
		
		leftIndex = 0;
		rightIndex = layers.Length - 1;
	}
	
	// Update is called once per frame
	void LateUpdate () {

		float deltaX = cameraTransform.position.x * 1.0f - lastCameraX * 1.0f;
		float deltaY = cameraTransform.position.y * 1.0f - lastCameraY * 1.0f;
		offsetY += deltaY;
		
		transform.position += Vector3.right * (parallaxSpeedX) * deltaX;
		transform.position += Vector3.up * deltaY * parallaxSpeedY;
		
		lastCameraX = cameraTransform.position.x * 1.0f;
		lastCameraY = cameraTransform.position.y * 1.0f;
		
		if (cameraTransform.position.x < layers[leftIndex].transform.position.x + viewZone) {
			scrollLeft();
		}
		if (cameraTransform.position.x > layers[rightIndex].transform.position.x - viewZone) {
			scrollRight();
		}
	}
	
	void scrollLeft() {
		int lastRight = rightIndex;
		layers[rightIndex].position -= Vector3.right * (backgroundSize);
//		layers[rightIndex].position += Vector3.up * offsetY;
		leftIndex = rightIndex;
		rightIndex -= 1;
		if (rightIndex < 0) {
			rightIndex = layers.Length - 1;
		}
	}
	
	void scrollRight() {
		int lastLeft = leftIndex;
		layers[leftIndex].position += Vector3.right * (backgroundSize);
//		layers[leftIndex].position += Vector3.up * offsetY;
		rightIndex = leftIndex;
		leftIndex += 1;
		if (leftIndex == layers.Length) {
			leftIndex = 0;
		}
	}
}
