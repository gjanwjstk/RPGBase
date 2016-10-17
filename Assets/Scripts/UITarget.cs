using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//수정일: 2016년 10월 17일-----------//

public class UITarget : MonoBehaviour
{
	[SerializeField]
	Player _Player;
	[SerializeField]
	GameObject _panel;
	[SerializeField]
	Slider _hp;
	[SerializeField]
	Text _name;
	[SerializeField]
	Text _hpText;
	
	void Update()
	{
		if (_Player == null) return;
		if (_Player._target != null && _Player._target != _Player)
		{
			_panel.SetActive(true);
			_hp.value = _Player._target.Hp_Precent();
			_name.text = _Player._target.ID + " (LV. " + _Player._target.Level + ")";
			_hpText.text = _Player._target.HP + "/" + _Player._target.HPMax;
		}
		else
		{
			_panel.SetActive(false);
		}
	}
}
