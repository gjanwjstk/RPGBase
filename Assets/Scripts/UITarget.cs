using UnityEngine;
using UnityEngine.UI;

public class UITarget : MonoBehaviour
{
    //--------------FIELD---------------------//
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
    //------------EVENTMETHOD-----------------//
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
    //--------------METHOD--------------------//
    //------작성자: 201202971 문지환----------//
}
