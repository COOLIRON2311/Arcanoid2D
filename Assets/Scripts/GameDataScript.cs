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

    public int pointsBonusP;
    public int fastBonusP;
    public int slowBonusP;
    public int ballBonusP;
    public int plusTwoBonusP;
    public int plusTenBonusP;

    public void setBonusesFrequency()
    {
        BlockScript.pointsBonusP = pointsBonusP;
        BlockScript.fastBonusP = fastBonusP;
        BlockScript.slowBonusP = slowBonusP;
        BlockScript.ballBonusP = ballBonusP;
        BlockScript.plusTwoBonusP = plusTwoBonusP;
        BlockScript.plusTenBonusP = plusTenBonusP;
    }

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
		PlayerPrefs.SetFloat("bgmValue", SoundMaster.instance.bgm.volume);
		PlayerPrefs.SetFloat("sfxValue", SoundMaster.instance.sfx.volume);
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
		level = PlayerPrefs.GetInt("level", 1);
        balls = PlayerPrefs.GetInt("balls", 6);
        points = PlayerPrefs.GetInt("points", 0);
        pointsToBall = PlayerPrefs.GetInt("pointsToBall", 0);
        bgm = PlayerPrefs.GetInt("bgm", 1) == 1;
        sfx = PlayerPrefs.GetInt("sfx", 1) == 1;
        SoundMaster.instance.bgm.volume = bgmValue;
        SoundMaster.instance.sfx.volume = sfxValue;
        setBonusesFrequency();
    }
}
