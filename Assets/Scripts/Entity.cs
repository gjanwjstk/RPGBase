using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Entity : MonoBehaviour
{
    //--------------FIELD---------------------//
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
	protected int _strength;
	public int Strength
	{
		get { return _strength; }
		set { _strength = Mathf.Clamp(value, 0, value); }
	}

	[SerializeField]
	protected int _intelligence;
	public int Intelligence
	{
		get { return _intelligence; }
		set { _intelligence = Mathf.Clamp(value, 0, value); }
	}

	[SerializeField]
	protected int _health;
	public int Health
	{
		get { return _health; }
		set { _health = Mathf.Clamp(value, 0, value); }
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
	public abstract int HPRecovery { get; }
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
	public abstract int Magic_Damage { get; }
	public abstract int Physics_Defense { get; }
	public abstract int Magic_Defense { get; }

	public float move_speed { set; get; }

	protected virtual void Recovery()
	{
		if (!enabled) return;

		if (_hp < HPMax)
		{
			_hp += HPRecovery;
			if (_hp >= HPMax) _hp = HPMax;
		}
		if (_mp < MPMax)
		{
			_mp += MPRecovery;
			if (_mp >= MPMax) _mp = MPMax;
		}
	}
	public float Hp_Precent() { return (_hp != 0 && HPMax != 0) ? (float)_hp / (float)HPMax : 0.0f; }
	public float Mp_Precent() { return (_mp != 0 && MPMax != 0) ? (float)_mp / (float)MPMax : 0.0f; }
    //------------EVENTMETHOD-----------------//
    //--------------METHOD--------------------//
   
    public void Init()
	{
		HP = HPMax;
		MP = MPMax;
        LevelMax = 99;

        InvokeRepeating("Recovery", 1.0f, 1.0f);
	}
    //특정 시간에 한번씩 반복 호출
    //InvokeRepeating(호출함수명, 최초실행)
    public void Add_Pos(Vector3 p) { transform.position += p; }
    public void Set_Pos(Vector3 p) { transform.position = p; }
    public Vector3 Get_Pos() { return transform.position; }
    //------작성자: 201202971 문지환----------//
}
