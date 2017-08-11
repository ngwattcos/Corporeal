using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {
	public Transform gameHUD;
	public Transform pauseScreen;
	public Transform musicCanvas;
	public Transform endLevelCanvas;
	
	public string nextLevel;
	
	AudioSource music;
	public GameObject player;
	
	public bool paused = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		music = musicCanvas.gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			pauseGame();
		}
	}
	
	public void pauseGame() {
		if (!pauseScreen.gameObject.activeInHierarchy) {
			pause();
		} else {
			unpause();
		}
	
	}
	
	public void pause() {
		gameHUD.gameObject.SetActive(false);
		pauseScreen.gameObject.SetActive(true);
		Time.timeScale = 0;
		player.transform.FindChild("Character Rig").GetComponent<CharacterManager>().enabled = false;
		player.GetComponent<CharacterUpdate>().enabled = false;
		player.GetComponent<DialogueController>().enabled = false;
		music.Pause();
		AudioListener.volume = 0;
		paused = true;
	}
	
	public void unpause() {
		gameHUD.gameObject.SetActive(true);
		pauseScreen.gameObject.SetActive(false);
		Time.timeScale = 1;
		player.transform.FindChild("Character Rig").GetComponent<CharacterManager>().enabled = true;
		player.GetComponent<CharacterUpdate>().enabled = true;
		player.GetComponent<DialogueController>().enabled = false;
		music.Play();
		AudioListener.volume = 1;
		paused = false;
	}
	
	public void restart() {
		unpause();
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadSceneAsync(scene.name);
	}
	
	public void mainMenu() {
		restart();
		SceneManager.LoadSceneAsync("Main_Menu");
	}
	
	public void endLevel(int _captured, int _used, int _remaining) {
		gameHUD.gameObject.SetActive(false);
		pauseScreen.gameObject.SetActive(false);
		endLevelCanvas.gameObject.SetActive(true);
		endLevelCanvas.gameObject.GetComponent<EndingReport>().numCaptured = _captured;
		endLevelCanvas.gameObject.GetComponent<EndingReport>().numUtilized = _used;
		endLevelCanvas.gameObject.GetComponent<EndingReport>().numRemaining = _remaining;
		Time.timeScale = 0;
		player.transform.FindChild("Character Rig").GetComponent<CharacterManager>().enabled = false;
		player.GetComponent<CharacterUpdate>().enabled = false;
		player.GetComponent<DialogueController>().enabled = false;
		music.Pause();
		AudioListener.volume = 0;
		paused = true;
	}
	
	public void loadNextLevel() {
		unpause();
		SceneManager.LoadScene(nextLevel);
	}
	
	
}
