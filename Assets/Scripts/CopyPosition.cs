using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    //--------------FIELD---------------------//
    public bool x, y, z = false;
	public Transform target = null;
    //------------EVENTMETHOD-----------------//
    void Update ()
	{
		if (target)
		{
			transform.position = new Vector3
			((x ? target.position.x : transform.position.x),
			(y ? target.position.y : transform.position.y),
			(z ? target.position.z : transform.position.z));
			//x 가 활성화 되어있지 않으면 그냥 카메라의 x값 쓴다
		}
	}
    //--------------METHOD--------------------//
    //------작성자: 201202971 문지환----------//
}
