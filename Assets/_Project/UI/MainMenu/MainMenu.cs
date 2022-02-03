using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayGame() {
		Debug.Log("play");
		SceneManager.LoadScene("Game");
	}

	public void QuitGame() {
		Debug.Log("quit");
		Application.Quit();
	}
}
