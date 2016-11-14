using UnityEngine;

public class Enemy : Entity
{
    //--------------FIELD---------------------//
    protected override void Recovery()
	{
		base.Recovery();

		int buff_bonus_hp = 0;
		HP += buff_bonus_hp;

		int buff_bonus_mp = 0;
		MP += buff_bonus_mp;
	}
	public override int HPMax
	{
		get
		{
			int base_hp = 100 + Level * 10;
			int equip_bonus = 0;
			int buff_bonus = 0;
			int attr_bonus = Health * 20;

			return base_hp + equip_bonus + buff_bonus + attr_bonus;
		}
	}
	public override int HPRecovery
	{
		get
		{
			int base_HpRec = Level;
			int equip_bonus = 0;
			int buff_bonus = 0;
			int attr_bonus = Health * 20;

			return base_HpRec + equip_bonus + buff_bonus + attr_bonus;
		}
	}
	public override int MPMax
	{
		get
		{
			int base_mp = 100 + Level * 10;
			int equip_bonus = 0;
			int buff_bonus = 0;
			int attr_bonus = Mana * 10;

			return base_mp + equip_bonus + buff_bonus + attr_bonus;
		}
	}
	public override int MPRecovery
	{
		get
		{
			int base_MpRec = Level;
			int equip_bonus = 0;
			int buff_bonus = 0;
			int attr_bonus = Mana;

			return base_MpRec + equip_bonus + buff_bonus + attr_bonus;
		}
	}
	public override int Physice_Damage
	{
		get
		{
			int base_dmg = 10 + Level;
			int equip_bonus = 0;
			int buff_bonus = 0;
			int attr_bonus = Strength * 2;

			return base_dmg + equip_bonus + buff_bonus + attr_bonus;
		}
	}
	public override int Magic_Damage
	{
		get
		{
			int base_dmg = 10 + Level * 2;
			int equip_bonus = 0;
			int buff_bonus = 0;
			int attr_bonus = Intelligence * 4;

			return base_dmg + equip_bonus + buff_bonus + attr_bonus;
		}
	}
	public override int Physice_Defense
	{
		get
		{
			int base_def = 5 + Level;
			int equip_bonus = 0;
			int buff_bonus = 0;
			int attr_bonus = Strength + Health;

			return base_def + equip_bonus + buff_bonus + attr_bonus;
		}
	}
	public override int Magic_Defense
	{
		get
		{
			int base_mdef = 5 + Level;
			int equip_bonus = 0;
			int buff_bonus = 0;
			int attr_bonus = Intelligence + Mana;

			return base_mdef + equip_bonus + buff_bonus + attr_bonus;
		}
	}
    //------------EVENTMETHOD-----------------//
    void Awake()
	{
		base.Init();
		_state = ENTITY_STATE.IDLE;
	}
    [SerializeField]
    Animator anim;
    public Animator Anim { get { return anim; } }
    Target targetMark;

    public void Set_TargetMarkOff()
    {
        if (targetMark == null)
            targetMark = GameObject.Find("TargetMark").GetComponent<Target>();
        targetMark.Target_Off();
        targetMark.transform.SetParent(null);
    }

    StateMachine<Enemy> stateMachine;

    //public void Hit_Physics_Damage(Player _player)
    //{
    //    anim.Play(null);
    //    anim.Play("Hit_Front");
    //    _state = ENTITY_STATE.HIT;

    //    HP -= _player.Physice_Damage - Physice_Defense;
    //    if (HP <= 0)
    //    {
    //        _player.Exp += _lv * 30;

    //        if (targetMark == null)
    //            targetMark = GameObject.Find("TargetMark").GetComponent<Target>();
    //        targetMark.Target_Off();
    //        targetMark.transform.SetParent(null);
    //        Destroy(gameObject);
    //    }
    //}
    //void Update()
    //{
    //    Update_Actions();
    //}
    //--------------METHOD--------------------//
    //void Update_Actions()
    //{
    //    switch (_state)
    //    {
    //        case ENTITY_STATE.HIT:
    //            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle_Base"))
    //                _state = ENTITY_STATE.IDLE;
    //            break;
    //    }
    //}
    public void Init(int _level)
    {
        _lv = _level;

        Strength += _lv * 5;
        Intelligence += _lv * 5;
        Health += _lv * 5;
        Mana += _lv * 5;

        if (_class == ENTITY_CLASS.MAGE) Intelligence += _lv * 5;
        if (_class == ENTITY_CLASS.WARRIOR) Strength += _lv * 5;

        HP = HPMax;
        MP = MPMax;

        stateMachine = new StateMachine<Enemy>();
        stateMachine.Init(this, Enemy01.States.Wander.Instance);
    }
    void Update()
    {
        if (stateMachine != null) stateMachine.Excute();
    }
    public void ChangeState(State<Enemy> _state)
    {
        if (stateMachine != null)
            stateMachine.ChangeState(_state);
    }
    public void RevertPreviousState()
    {
        if (stateMachine != null)
            stateMachine.RevertToPreviousState();
    }
    public State<Enemy> GetCurrentState()
    {
        return stateMachine.GetcurrentState();
    }
    //------작성자: 201202971 문지환----------//
}
