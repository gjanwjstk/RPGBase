using UnityEngine;
using UnityEngine.UI;

public class UIHPMP : MonoBehaviour
{
    //--------------FIELD---------------------//
    [SerializeField]
	Player _player;
	[SerializeField]
	Slider _hp;
	[SerializeField]
	Slider _mp;
	[SerializeField]
	Text _hpText;
	[SerializeField]
	Text _mpText;
    //------------EVENTMETHOD-----------------//
    void Update()
	{
		if (_player.transform == null)
			return;

		_hp.value = _player.Hp_Precent();
		_hpText.text = _player.HP + "/" + _player.HPMax;
		_mp.value = _player.Mp_Precent();
		_mpText.text = _player.MP + "/" + _player.MPMax;
	}
    //--------------METHOD--------------------//
    //------작성자: 201202971 문지환----------//
}
