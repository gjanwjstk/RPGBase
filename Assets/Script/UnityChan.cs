using UnityEngine;
using System.Collections;

public enum CHAR_STATE {  NONE=-1, IDLE=0, RUN, ATTACK }

public class UnityChan : MonoBehaviour {

	private Animator anim;
	private Vector3 goal_pos;
	private CHAR_STATE state;
	private float speed;
	private int atk_numeric;

	void Awake()
	{
		anim = GetComponent<Animator>();
		goal_pos = Vector3.zero;
		state = CHAR_STATE.IDLE;
		speed = 3.0f;
		atk_numeric = 0;
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Update_Inputs();
		Update_Actions();
	}
	void Update_Inputs()
	{
		Ray ray;
		RaycastHit hit;

		if (Input.GetMouseButton(0))
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{
				anim.Play("Run");
				anim.SetBool("is_run", true);

				state = CHAR_STATE.RUN;
				goal_pos = hit.point;

				transform.localRotation = Quaternion.LookRotation(goal_pos - Get_Pos());
			}
		}

		if (Input.GetKeyDown("1")) 
		{
			if (state == CHAR_STATE.RUN)
				anim.SetBool("is_run", false);

			state = CHAR_STATE.ATTACK;
			anim.SetTrigger("is_attack_jab");
		}
		else if (Input.GetKeyDown("2"))
		{
			if (state == CHAR_STATE.RUN)
				anim.SetBool("is_run", false);

			state = CHAR_STATE.ATTACK;
			anim.SetTrigger("is_attack_hikick");
		}
	}
	void Update_Actions()
	{
		switch (state)
		{
			case CHAR_STATE.IDLE:
				break;

			case CHAR_STATE.RUN:
				Vector3 move_pos = Vector3.zero;
				if (Vector3.Distance(goal_pos, Get_Pos()) > .02f)
					move_pos = Vector3.Normalize(goal_pos - Get_Pos());
				else
				{
					anim.Play("Idle");
					anim.SetBool("is_run", false);
					Set_Pos(goal_pos);
					state = CHAR_STATE.IDLE;
				}
				Add_Pos(move_pos * speed * Time.deltaTime);
				break;

			case CHAR_STATE.ATTACK:
				if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) ;
				{
					state = CHAR_STATE.IDLE;
				}
				break;

			case CHAR_STATE.NONE:
			default:
				break;
		}
	}
	public void Add_Pos(Vector3 p)
	{
		transform.position += p;
	}
	public void Set_Pos(Vector3 p)
	{
		transform.position = p;
	}
	public Vector3 Get_Pos()
	{
		return transform.position;
	}
}
