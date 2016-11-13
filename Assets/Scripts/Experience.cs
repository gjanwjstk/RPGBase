using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    //--------------FIELD---------------------//
    [SerializeField]
	Player _player;
	[SerializeField]
	Slider _exp;
	[SerializeField]
	Text _expText;
    //------------EVENTMETHOD-----------------//
    void Update()
	{
		if (_player.transform == null)
			return;

		_exp.value = _player.Exp_Percent();
		_expText.text = "Lv." + _player.Level + " (" + (_player.Exp_Percent() * 100.0f).ToString("F2") + "%)";
	}
    //--------------METHOD--------------------//
    //------작성자: 201202971 문지환----------//
}
