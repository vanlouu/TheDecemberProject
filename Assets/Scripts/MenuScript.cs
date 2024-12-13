using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
	public GameObject bgImage;
	public void StartGame()
	{
		SceneManager.LoadScene("MainScene");
	}

	public void DontStartGame()
	{
		Application.Quit();
	}

	public void Resume()
    {
		Time.timeScale = 1;
		bgImage.SetActive(false);
    }

	public void Disable(GameObject obj)
    {
		obj.SetActive(false);
    }
}
