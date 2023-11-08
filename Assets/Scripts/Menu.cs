using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public void StartGame()
	{
		SceneManager.LoadScene("MainScene");
	}

	public void NewGame()
	{

		var gameData = GameDataObject.instance.GameData;
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
