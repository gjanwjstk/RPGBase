using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{

	[SerializeField]
	private Transform to_portal;

	public void Move_To(Player p)
	{
		p.Set_Pos(to_portal.position);
	}
}
