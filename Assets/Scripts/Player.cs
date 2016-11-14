using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : Entity
{
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
    //--------------FIELD---------------------//
    [Header("Attribute Points")]
	[SerializeField]
	private int _att_point;
	public int Att_Point
	{
		get { return _att_point; }
		set { _att_point = Mathf.Clamp(value, 0, value); }
	}
	[Header("Experience")]
	[SerializeField]
	long _exp;
    [SerializeField] Target targetMark;
    public long Exp
	{
		get { return _exp; }
		set
		{
			_exp = value;
			if (_exp >= ExpMax)
			{
				_exp -= ExpMax;

				if (Level * 0.3 < 1.0) Att_Point++;
				else Att_Point = Att_Point + (int)(Level * 0.3);
				_lv++;

				HP = HPMax;
				MP = MPMax;
			}
		}
	}
	public float Exp_Percent() { return (Exp != 0 && ExpMax != 0) ? (float)Exp / (float)ExpMax : 0.0f; }
	public long ExpMax { get { return Level * Level * 100; } }

	private Animator anim;
	private float idle_time;
	private new float move_speed;
	private Vector3 goal_pos;
	private GameObject target_portal;
	private Rigidbody _rigid;
    //------------EVENTMETHOD-----------------//
    void OnCollisionEnter(Collision col)
	{
		if (target_portal == null)
			return;
		if (!col.gameObject.name.Contains("Portal"))
			return;

		Portal portal = col.gameObject.GetComponent<Portal>() as Portal;
		portal.Move_To(this);

		target_portal = null;

		anim.Play("Idle_Base");
		_state = ENTITY_STATE.IDLE;
	}
	void OnCollisionExit(Collision col)
	{
		_rigid.isKinematic = true;
	}
	void Awake()
	{
		base.Init();

		anim = GetComponent<Animator>();
		_state = ENTITY_STATE.IDLE;
		idle_time = .0f;

		move_speed = 3.0f;
		goal_pos = Vector3.zero;

		target_portal = null;
		_rigid = GetComponent<Rigidbody>();
	}
	void Update()
	{
		Update_Inputs();
		Update_Actions();
	}
    //--------------METHOD--------------------//
    void Update_Inputs()
	{
		Ray ray;
		RaycastHit hit;

		if (Input.GetMouseButton(0))
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{
				var entity = hit.transform.GetComponent<Entity>() as Entity;
				if (entity && (entity !=this))
                {
                    if (entity != null&&entity.GetComponent<Enemy>().GetCurrentState() == Enemy01.States.Die.Instance)
                    {
                        _target = null;
                        return;
                    }
                    if (_target == entity)
                    {
                        if (Vector3.Distance(_target.Get_Pos(),Get_Pos()) >=0.85f)
                        {
                            if (Input.GetKey(KeyCode.LeftShift))
                            {
                                move_speed = .5f;
                                anim.Play("Run_SilentWalk");
                            }
                            else
                            {
                                move_speed = 3.0f;
                                anim.Play("Run_Base");
                            }
                            _state = ENTITY_STATE.MOVE;
                            goal_pos = _target.Get_Pos();
                        }
                        else
                        {
                            Base_Attack();
                        }
                        transform.localRotation = Quaternion.LookRotation(_target.Get_Pos() - Get_Pos());
                        return;
                    }
					_target = entity;
                    targetMark.Target_On(_target.Get_Pos());
                    targetMark.transform.SetParent(_target.transform);
				}
				//else
				//_target = null;
    //            targetMark.Target_Off();
    //            targetMark.transform.SetParent(null);
			}
		}


		if (Input.GetMouseButtonDown(1))
		{
			if (Input.GetKey(KeyCode.LeftControl))
				return;

			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform.name.Equals("PC_01"))
					return;
				if (hit.transform.name.Contains("Portal"))
					target_portal = hit.transform.gameObject;
				else
					target_portal = null;

				if (Input.GetKey(KeyCode.LeftShift))
				{
					move_speed = 0.5f;
					anim.Play("Run_SilentWalk");
				}
				else
				{
					move_speed = 3.0f;
					anim.Play("Run_Base");
				}

				_state = ENTITY_STATE.MOVE;
				goal_pos = hit.point;

				transform.localRotation = Quaternion.LookRotation(goal_pos - Get_Pos());
			}
		}
	}
	void Update_Actions()
	{
		switch (_state)
		{
			case ENTITY_STATE.IDLE:
				if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle_Base"))
				{
					if (idle_time < 5.0f) idle_time += Time.deltaTime;
					else
					{
						idle_time = .0f;
						anim.SetTrigger("idle_lookingAround");
					}
				}

				if (Input.GetKeyDown(KeyCode.P))
				{
					anim.SetBool("idle_is_tired", !anim.GetBool("idle_is_tired"));
				}
				break;
			case ENTITY_STATE.MOVE:
				Vector3 move_pos = Vector3.zero;
				if (Input.GetKey(KeyCode.LeftShift))
				{
					move_speed = .5f;
					anim.Play("Run_SilentWalk");
				}
				else
				{
					move_speed = 3.0f;
					anim.Play("Run_Base");
				}

				if (Vector3.Distance(goal_pos, Get_Pos()) > 0.1f)
					move_pos = Vector3.Normalize(goal_pos - Get_Pos());
				else
				{
					anim.Play("Idle_Base");

					Set_Pos(goal_pos);
					_state = ENTITY_STATE.IDLE;
				}
				Add_Pos(move_pos * move_speed * Time.deltaTime);

				if (_rigid.isKinematic == true)
					_rigid.isKinematic = false;
				break;
		}
	}
    public void Base_Attack()
    {
        anim.Play("Attack_Base");
        _state = ENTITY_STATE.ATTACK;
    }
    public void Update_Physics_Attack()
    {
        Enemy enemy = _target.GetComponent<Enemy>() as Enemy;
        //enemy.Hit_Physics_Damage(this); 
        if (enemy._target != this)
            enemy._target = this;
        enemy.ChangeState(Enemy01.States.Hit.Instance);
    }
    public void End_Physics_Attack()
    {
        anim.Play("Idle_Base");
        _state = ENTITY_STATE.IDLE;
    }
    //------작성자: 201202971 문지환-----------//
}
