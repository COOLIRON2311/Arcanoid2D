using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	GameDataScript gameData;

	void Start()
	{
		gameData = GameDataObject.instance.GameData;
		gameData.Load();
	}

	public void StartGame()
	{
		SceneManager.LoadScene("MainScene");
		gameData.Save();
	}

	public void NewGame()
	{
		gameData.Reset();
		SceneManager.LoadScene("MainScene");
		Time.timeScale = 1;
	}

	public void Resume()
	{
		transform.parent.gameObject.SetActive(false);
		Cursor.visible = false;
		Time.timeScale = 1;
		SoundMaster.instance.bgm.Play();
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
