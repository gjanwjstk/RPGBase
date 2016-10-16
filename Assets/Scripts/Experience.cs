using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
	[SerializeField]
	Player _player;
	[SerializeField]
	Slider _exp;
	[SerializeField]
	Text _expText;
	void Update()
	{
		if (_player.transform == null)
			return;

		_exp.value = _player.Exp_Percent();
		_expText.text = "Lv." + _player.Level + " (" + (_player.Exp_Percent() * 100.0f).ToString("F2") + "%)";
	}
}
