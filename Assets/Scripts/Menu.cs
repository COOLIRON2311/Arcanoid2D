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
}
