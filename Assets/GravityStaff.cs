using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//using UnityEditor;

public class GravityStaff : MonoBehaviour {

	// public String name;
	CharacterUpdate cu;
	public Switch sw;
	
	Switch onSwitch;
	
	public AudioSource gravSound;
	public AudioClip gravSoundClip;
	
	Light onLight;
	
	// Use this for initialization
	void Start () {
		cu = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterUpdate>();
		sw = transform.parent.GetComponent<Switch>();
		onSwitch = gameObject.AddComponent<Switch>();
		
		onLight = gameObject.AddComponent<Light>();
		onLight.color = new Color(1.0f, 0, 1.0f);
		onLight.range = 7;
		onLight.intensity = 5;
		
		gravSound = gameObject.AddComponent<AudioSource>();
		gravSound.clip = gravSoundClip;
		gravSound.loop = true;
		gravSound.playOnAwake = false;
	}
	
	// Update is called once per frame
	void Update () {
//		GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterHealth>().damage(1);
		
		onLight.enabled = sw.getEnabled() && onSwitch.getEnabled();
		
		if (!sw.getEnabled()) {
			gravSound.Stop();
		}
	}
	
	void OnTriggerStay2D(Collider2D other) {
			
		if (sw.getEnabled()) {
			if (sw.getEnabled() && Input.GetButton("Fire1")) {
				onSwitch.enableSwitch();
				
				
				
				if (other.gameObject.GetComponent<Rigidbody2D>() != null) {
					if (other.gameObject.GetComponent<EnemyHealth>() != null &&
						other.gameObject.GetComponent<EnemyHealth>().maxHealth < 200 ||
						other.gameObject.GetComponent<EnemyHealth>() == null)
					{
						Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
						Vector2 dirVec = other.gameObject.transform.position - gameObject.transform.position;
						float neg = 1;
						if (Input.GetKey(KeyCode.LeftShift)) {
							neg = -1;
						}
						rb.AddForce(neg * -dirVec.normalized * rb.mass * 15);

						drawLine(transform.position, other.gameObject.transform.position, new Color(1f, 0f, 1f));
					}
					
				}
			} else {
				onSwitch.disableSwitch();
			}
			
			
		}
		
		if (onSwitch.wasEnabled()) {
			gravSound.Play();
		}

		if (onSwitch.wasDisabled()) {
			gravSound.Stop();
		}
		
	}
	
	void drawLine(Vector3 start, Vector3 end, Color color, float duration = 0.02f) {
		GameObject myLine = new GameObject();
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
//		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.SetColors(color, color);
		lr.SetWidth(0.01f, 0.01f);
		lr.SetPosition(0, new Vector3(start.x, start.y, start.z - 2));
		lr.SetPosition(1, new Vector3(end.x, end.y, end.z - 2));
		GameObject.Destroy(myLine, duration);
	}
		
	
}
