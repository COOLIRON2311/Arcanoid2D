using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBar : MonoBehaviour
{
	public bool isGlobal = false;

	Slider _slider;
	SoundMaster _soundMaster;
	

	private void Start()
	{
		_slider = GetComponentInChildren<Slider>();
		_soundMaster = FindObjectOfType<SoundMaster>();
		if (isGlobal)
			_slider.value = _soundMaster.sfx.volume;
		else
			_slider.value = _soundMaster.bgm.volume;

		_slider.onValueChanged.AddListener(delegate { sliderChanged(); });
	}

	private void sliderChanged()
	{
		if (isGlobal)
			_soundMaster.sfx.volume = _slider.value;
		else
			_soundMaster.bgm.volume = _slider.value;
	}
}
