using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    //--------------FIELD---------------------//
    [SerializeField]
	private Transform to_portal;
    //------------EVENTMETHOD-----------------//
    //--------------METHOD--------------------//
    public void Move_To(Player p)
	{
		p.Set_Pos(to_portal.position);
	}
    //------작성자: 201202971 문지환-----------//
}
