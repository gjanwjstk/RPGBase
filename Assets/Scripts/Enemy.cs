using UnityEngine;

public class Enemy : Entity
{
    //--------------FIELD---------------------//
    protected override void Recovery()
    {
        base.Recovery();

        //int buff_bonus_hp = 0;
        //HP += buff_bonus_hp;

        //int buff_bonus_mp = 0;
        //MP += buff_bonus_mp;
    }
    public override int HPMax
    {
        get
        {
            int base_hp = 100 + Level * 10;
            int equip_bonus = 0;
            int buff_bonus = 0;
            int attr_bonus = Health;
            // int attr_bonus = Health * 20;

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
    public override int Physics_Damage
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
            int base_mdmg = 10 + Level * 2;
            int equip_bonus = 0;
            int buff_bonus = 0;
            int attr_bonus = Intelligence * 4;

            return base_mdmg + equip_bonus + buff_bonus + attr_bonus;
        }
    }
    public override int Physics_Defense
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

        move_speed = .5f;
        _state = ENTITY_STATE.IDLE;
    }
    public Vector3 Wander_Target_Pos { get; set; }

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
        stateMachine.Set_GlobalState(Enemy01.States.GlobalState.Instance);
    }
    void Update()
    {
        if (stateMachine != null) stateMachine.Execute();
    }
    public void ChangeState(State<Enemy> _state)
    {
        if (stateMachine != null)
            stateMachine.ChangeState(_state);
    }
    public void RevertToPreviousState()
    {
        if (stateMachine != null)
            stateMachine.RevertToPreviousState();
    }
    public State<Enemy> GetCurrentState()
    {
        return stateMachine.GetCurrentState();
    }

    public int attack_state { set; get; }
    public void Attack_State(int _state)
    {
        attack_state = _state;
    }
    public void Damage_Popup(string _dmg)
    {
        GameObject clone = Instantiate(damage_popup_prefab) as GameObject;

        Bounds bounds = GetComponent<Collider>().bounds;
        clone.transform.position = new Vector3(bounds.center.x, bounds.max.y, bounds.center.z);

        clone.GetComponent<TextMesh>().text = _dmg;
    }
}
