using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMinimap : MonoBehaviour
{
	[SerializeField]
	private float zoomMin = 2.0f;
	[SerializeField]
	private float zoomMax = 10.0f;
	[SerializeField]
	private float zoom_StepSize = 1.0f;
	[SerializeField]
	private Text Level_name;
	[SerializeField]
	private Button btn_plus;
	[SerializeField]
	private Button btn_minus;
	[SerializeField]
	private Camera minimap_cam;
	
	void Update ()
	{
		Level_name.text = SceneManager.GetActiveScene().name;
	}
	public void ZoomIn()
	{
		minimap_cam.orthographicSize = Mathf.Max(minimap_cam.orthographicSize - zoom_StepSize, zoomMin);
	}
	public void ZoomOut()
	{
		minimap_cam.orthographicSize = Mathf.Max(minimap_cam.orthographicSize - zoom_StepSize, zoomMax);
	}
}
