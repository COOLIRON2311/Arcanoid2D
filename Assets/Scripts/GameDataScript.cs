using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = " Game Data", order = 51)]
public class GameDataScript : ScriptableObject
{
    public bool restoreOnStart;
    public bool bgm = true;
    public bool sfx = true;
	public float bgmValue = 0.25f;
	public float sfxValue = 1.0f;
	public int level = 1;
    public int balls = 6;
    public int points = 0;
    public int pointsToBall = 0;

    public void Reset()
    {
        level = 1;
        balls = 6;
        points = 0;
        pointsToBall = 0;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("level", level);
		PlayerPrefs.SetFloat("bgmValue", bgmValue);
		PlayerPrefs.SetFloat("sfxValue", sfxValue);
		PlayerPrefs.SetInt("balls", balls);
        PlayerPrefs.SetInt("points", points);
        PlayerPrefs.SetInt("pointsToBall", pointsToBall);
        PlayerPrefs.SetInt("bgm", bgm ? 1 : 0);
        PlayerPrefs.SetInt("sfx", sfx ? 1 : 0);
        PlayerPrefs.Save(); // just in case
    }

    public void Load()
    {
		bgmValue = PlayerPrefs.GetFloat("bgmValue", 0.25f);
		sfxValue = PlayerPrefs.GetFloat("sfxValue", 1.0f);
        SoundMaster.instance.bgm.volume = bgmValue;
        SoundMaster.instance.sfx.volume = sfxValue;
		level = PlayerPrefs.GetInt("level", 1);
        balls = PlayerPrefs.GetInt("balls", 6);
        points = PlayerPrefs.GetInt("points", 0);
        pointsToBall = PlayerPrefs.GetInt("pointsToBall", 0);
        bgm = PlayerPrefs.GetInt("bgm", 1) == 1;
        sfx = PlayerPrefs.GetInt("sfx", 1) == 1;
    }
}
