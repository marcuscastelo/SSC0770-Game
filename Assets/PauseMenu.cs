using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
	public GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
		{
			Debug.Log("Tecla P presionada.");
			if(IsPaused == true)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
    }

	public void Resume()
	{
		PauseMenuUI.SetActive(false);
		Time.timeScale = 1.0f;
		IsPaused = false;
	}

	void Pause()
	{
		PauseMenuUI.SetActive(true);
		Time.timeScale = 0.0f;
		IsPaused = true;
	}

	public void Menu()
	{
		Time.timeScale = 1.0f;
		SceneManager.LoadScene("MainMenu");
	}
}
