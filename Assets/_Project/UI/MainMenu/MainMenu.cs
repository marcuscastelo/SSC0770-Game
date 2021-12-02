using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private GameObject startButton;
	[SerializeField] private GameObject controlsButton;
	[SerializeField] private GameObject quitButton;

	void Start()
	{
		bool isWebGL = Application.platform == RuntimePlatform.WebGLPlayer;
		startButton.SetActive(true);
		controlsButton.SetActive(true);
		quitButton.SetActive(!isWebGL);
	}

	public void PlayGame() {
		Debug.Log("play");
		SceneManager.LoadScene("Game");
	}

	public void QuitGame() {
		Debug.Log("quit");
		Application.Quit();
	}

}
