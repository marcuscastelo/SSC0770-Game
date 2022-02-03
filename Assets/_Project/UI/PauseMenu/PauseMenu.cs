using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Hypnos.Audio;
using Zenject;

public class PauseMenu : UILayer
{
	public GameObject[] objectsToDisable;
	bool[] objectsToDisableOldState;

    bool paused = false;

	private AudioSystem _audioSystem;

	[Inject]
	public void Construct(AudioSystem audioSystem)
	{
		this._audioSystem = audioSystem;
	}

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
			if (Time.timeScale == 0) {
				Debug.LogWarning("[PauseMenu] SetPaused(true) called with Time.timeScale already set to 0");
				return;
			}

			_audioSystem.PauseAllGlobals();
			this.Show();
			Time.timeScale = 0.0f;
			paused = true;
			DisableObjects();
		}
		else
		{
			if (Time.timeScale != 0) {
				Debug.LogWarning("[PauseMenu] SetPaused(false) called with Time.timeScale already set to non-zero");
				return;
			}

			_audioSystem.UnpauseAllGlobals();
			this.Hide();
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
