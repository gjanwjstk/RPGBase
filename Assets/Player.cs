using UnityEngine;
using System.Collections;

public enum PLAYER_STATE { IDLE=0, MOVE}

public class Player : MonoBehaviour
{
	private Animator anim;
	private PLAYER_STATE player_state;
	private float idle_time;

	private float move_speed;
	private Vector3 goal_pos;

	void Awake()
	{
		anim = GetComponent<Animator>();
		player_state = PLAYER_STATE.IDLE;
		idle_time = .0f;

		move_speed = 3.0f;
		goal_pos = Vector3.zero;
	}
	void Update ()
	{
		Update_Inputs();
		Update_Actions();
	}

	void Update_Inputs()
	{
		Ray ray;
		RaycastHit hit;

		if (Input.GetMouseButtonDown(1))
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
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

				player_state = PLAYER_STATE.MOVE;
				goal_pos = hit.point;

				transform.localRotation = Quaternion.LookRotation(goal_pos - Get_Pos());
			}
		}
	}
	void Update_Actions()
	{
		switch (player_state)
		{
			case PLAYER_STATE.IDLE:
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

			case PLAYER_STATE.MOVE:
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
				
				Vector3 move_pos = Vector3.zero;
				if (Vector3.Distance(goal_pos, Get_Pos()) > .1f)
					move_pos = Vector3.Normalize(goal_pos - Get_Pos());
				else
				{
					anim.Play("Idle_Base");

					Set_Pos(goal_pos);
					player_state = PLAYER_STATE.IDLE;
				}
				Add_Pos(move_pos * move_speed * Time.deltaTime);
				break;
		}
	}
	public void Add_Pos(Vector3 p) { transform.position += p; }
	public void Set_Pos(Vector3 p) { transform.position = p; }
	public Vector3 Get_Pos() { return transform.position; }
}
