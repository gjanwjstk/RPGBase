using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
	[SerializeField]
	private Transform target;
	private float distance;
	private float xSpeed, ySpeed;
	private float yMinLimit, YMaxLimit;
	private float x, y;
	private Vector3 position;
	private Quaternion rotation;

	void Awake()
	{
		distance = 10.0f;
		xSpeed = 250.0f;
		ySpeed = 120.0f;
		yMinLimit = 5.0f;
		YMaxLimit = 80.0f;

		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		rotation = Quaternion.Euler(y, x, .0f);
	}
	void Update()
	{
		if (!target)
			return;

		position = rotation * new Vector3(.0f, .0f, -distance) + target.position;
		transform.rotation = rotation;
		transform.position = position;

		if (Input.GetAxis("Mouse ScrollWheel") > .0f)
		{
			if (distance > 2.0f)
				distance -= .2f;
			else
				distance = 2.0f;
		}
		if (Input.GetAxis("Mouse ScrollWheel") < .0f)
		{
			if (distance < 10.0f)
				distance += .2f;
			else
				distance = 10.0f;
		}
		if (Input.GetMouseButton(1))
		{
			if (!Input.GetKey(KeyCode.LeftControl))
				return;
			x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
			y += Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
			y = ClampAngle(y, yMinLimit, YMaxLimit);
			rotation = Quaternion.Euler(y, x, .0f);
		}
	}
	float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp(angle, min, max);
	}
}
