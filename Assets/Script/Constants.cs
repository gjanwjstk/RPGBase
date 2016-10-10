using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {

	public enum ENTIT_STATE
	{
		IDLE=0, MOVE, ATTACK, HIT, CASTING, DIE,
	}
	public enum ENTITY_CLASS
	{
		NOOB=0, MAGE, MARRIOR, ARCHER,
	}

}
