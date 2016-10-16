using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
	[SerializeField]
	private Transform target;
	private float distance;
	private float xSpeed, ySpeed;
	private float yMinLimit, yMaxLimit;
	private float x, y;
	private Vector3 position;
	private Quaternion rotation;

	void Awake()
	{
		distance = 10.0f;
		xSpeed = 250.0f;
		ySpeed = 120.0f;
		yMinLimit = 5.0f;
		yMaxLimit = 80.0f;

		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		rotation = Quaternion.Euler(y, x, .0f);
	}
}
