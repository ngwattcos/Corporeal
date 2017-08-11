using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {
	
	public GameObject dialogBox;
	public Text dialogText;
	public Text speaker;

	private List<KeyValuePair<string[], float[]>> dialogues;
	
	// Use this for initialization
	void Start () {
		dialogues = new List<KeyValuePair<string[], float[]>>();
		
		dialogBox = GameObject.FindGameObjectWithTag("DialogBox");
		dialogText = GameObject.FindGameObjectWithTag("Dialog").GetComponent<Text>();
		speaker = GameObject.FindGameObjectWithTag("Speaker").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		KeyValuePair<string[], float[]> currentDialogue;
		
		if (dialogues.Count > 0) {
			dialogBox.SetActive(true);
			currentDialogue = dialogues.ElementAt(0);
			currentDialogue.Value[1] += Time.deltaTime;
			
			dialogText.text = currentDialogue.Key[0];
			speaker.text = currentDialogue.Key[1];
			
			if (currentDialogue.Value[1] > currentDialogue.Value[0] || Input.GetKeyDown(KeyCode.Return)) {
				// remove this,
				// and wait for the next frame to set the panel text and whether it is enabled1
				dialogues.RemoveAt(0);
			}
		} else {
			dialogBox.SetActive(false);
		}
		
		
	}
	
	public void addDialogue(String _dial, String _speaker, float _time) {
		KeyValuePair<string[], float[]> newDialogue = new KeyValuePair<string[], float[]>(
			new string[] {_dial, _speaker},
			new float[] {_time, 0.0f});
		dialogues.Add(newDialogue);
	}
}
