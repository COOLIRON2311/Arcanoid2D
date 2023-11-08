using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBar : MonoBehaviour
{
	public bool isGlobal = false;

	Slider slider;
	GameDataScript gameData;


	private void Start()
	{
		gameData = GameDataObject.instance.GameData;
		slider = GetComponentInChildren<Slider>();
		if (isGlobal)
			slider.value = gameData.bgmValue;
		else
			slider.value = gameData.sfxValue;

		slider.onValueChanged.AddListener(delegate { sliderChanged(); });
	}

	private void sliderChanged()
	{
		if (isGlobal)
			SoundMaster.instance.bgm.volume = slider.value;
		else
			SoundMaster.instance.sfx.volume = slider.value;
	}
}
