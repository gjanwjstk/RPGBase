using UnityEngine;
using System.Collections;

public class CopyPosition : MonoBehaviour
{
	public bool x, y, z = false;
	public Transform target = null;
	
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
}
