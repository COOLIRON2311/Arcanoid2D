using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBar : MonoBehaviour
{
	public bool isGlobal = false;

	Slider slider;


	private void Start()
	{
		slider = GetComponentInChildren<Slider>();
		if (isGlobal)
			slider.value = SoundMaster.instance.bgm.volume;
		else
			slider.value = SoundMaster.instance.sfx.volume;

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
