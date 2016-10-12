using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public abstract class Entity : MonoBehaviour
{
	[Header("Target")]
	public Entity _target;

	[Header("ID/Class/Level/State")]
	[SerializeField]
	protected string _id;
	public string ID { get { return _id; } }

	[SerializeField]
	protected ENTITY_CLASS _class; 
	public ENTITY_CLASS Class { get { return _class; } }

	[SerializeField]
	protected int _lv;
	public int Level { get { return _lv; } }
	public int LevelMax { private set; get; }

	[SerializeField]
	protected ENTITY_STATE _state;
	public ENTITY_STATE State { get { return _state; } }

	[Header("Attributes")]
	[SerializeField]
	protected int _stregth;
	public int Strength
	{
		get { return _stregth; }
		set { _stregth = Mathf.Clamp(value, 0, value); }
	}
	[SerializeField]
	protected int _intelligence;
	public int Intelligence
	{
		get { return _intelligence; }
		set { _intelligence = Mathf.Clamp(value, 0, value); }
	}
	[SerializeField]
	protected int _mana;
	public int Mana
	{
		get { return _mana; }
		set { _mana = Mathf.Clamp(value, 0, value); }
	}
	[Header("HP/MP")]
	[SerializeField]
	protected bool invincible = false;
	[SerializeField]
	protected int _hp = 100;

	public int HP
	{
		get { return _hp; }
		set { _hp = Mathf.Clamp(value, 0, HPMax); }
	}
	public abstract int HPRevovery { get; }
	public abstract int HPMax { get; }

	[SerializeField]
	protected int _mp = 100;

	public int MP
	{
		get { return _mp; }
		set { _mp = Mathf.Clamp(value, 0, MPMax); }
	}
	public abstract int MPRecovery { get; }
	public abstract int MPMax { get; }
	public abstract int Physics_Damage { get; }
	public abstract int Physics_Defense { get; }
	public abstract int Magic_Damage { get; }
	public abstract int Magic_Defense { get; }

	public float move_speed { protected set; get; }

	//Entity의 HP/MP 회복 함수
	protected virtual void Recovery()
	{
		if (!enabled) return;

		if (_hp < HPMax)
		{
			_hp += HPRevovery;
			if (_hp >= HPMax) _hp = HPMax;
		}
		if ( _mp < MPMax)
		{
			_mp += MPRecovery;
			if(_mp >= HPMax) _mp = MPMax;
		}
	}
	public float HP_Percent()
	{
		return
			(_hp != 0 && HPMax != 0) ? (float)_hp / (float)HPMax : 0.0f;
	}
	public float MP_Percent()
	{
		return
			(_mp != 0 && MPMax != 0) ? (float)_mp / (float)MPMax : 0.0f;
	}

	public void Init()
	{
		LevelMax = 99;

		HP = HPMax;
		MP = MPMax;

		InvokeRepeating("Recovery", 1.0f, 1.0f);
		//특정 시간에 한번씩 반복 호출
		//InvokeRepeating(호출함수명, 최초실행 
	}
}
