﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void startGame() {
		SceneManager.LoadSceneAsync("Level_01");
	}
	
	public void quit() {
		Application.Quit();
	}
}
