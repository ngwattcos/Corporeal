using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class CharacterFollow : MonoBehaviour {
	
	// Use this for initialization
	public Transform target;
	public float speed = 10f;
	public Vector3 delta;
	public Vector2 delta2;
	// float range;
	
	public Vector3 pos;
	
	public Vector3 velocity;
	
	Vector3 offset;
	
	void Start () {
		speed = 10f;
		offset = transform.position - target.position + new Vector3(0, 2, 0);
		// range = Vector2.Distance(transform.position, target.position);
		
		transform.position = target.position + offset;
		pos = transform.position;
		velocity = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = target.position + offset;
		
		
		pos = transform.position;
//		transform.position = Vector2.MoveTowards(transform.position, target.position + offset, 1 * Time.deltaTime);
//		transform.position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(0, 0, -10),  speed * Time.deltaTime);
		delta = target.position - transform.position;
		delta = Vector3.ClampMagnitude(delta, speed);
		delta2 = delta;
//		transform.position += (Vector3) delta2;
//		transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * 5);
//		transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, 0.5f);
		
//		transform.position.Set(transform.position.x, transform.position.y, -40f);
	}
}
