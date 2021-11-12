using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public GameObject[] objectsToDisable;
	bool[] objectsToDisableOldState;

    bool paused = false;

	void DisableObjects()
	{
		objectsToDisableOldState = new bool[objectsToDisable.Length];
		for (int i = 0; i < objectsToDisable.Length; i++)
		{
			objectsToDisableOldState[i] = objectsToDisable[i].activeSelf;
			objectsToDisable[i].SetActive(false);
		}
	}

	void ReEnableObjects()
	{
		for (int i = 0; i < objectsToDisable.Length; i++)
		{
			objectsToDisable[i].SetActive(objectsToDisableOldState[i]);
		}
		objectsToDisableOldState = null;
	}

	public void SetPaused(bool pause)
	{
		if (pause)
		{
			gameObject.SetActive(true);
			Time.timeScale = 0.0f;
			paused = true;
			DisableObjects();
		}
		else
		{
			gameObject.SetActive(false);
			Time.timeScale = 1.0f;
			paused = false;
			ReEnableObjects();
		}
	}

	public void Pause()
	{
		SetPaused(true);
	}

	public void Unpause()
	{
		SetPaused(false);
	}

	public void TogglePause()
	{
		SetPaused(!paused);
	}

	public void LoadMainMenu()
	{
		Unpause();
		SceneManager.LoadScene("MainMenu");
	}
}
