using UnityEngine;

public class UIKeepInScreen : MonoBehaviour
{
    //--------------FIELD---------------------//
    //------------EVENTMETHOD-----------------//
    void Update ()
	{
		Rect r = GetComponent<RectTransform>().rect;

		Vector2 minworld = transform.TransformPoint(r.min);
		Vector2 maxworld = transform.TransformPoint(r.max);
		Vector2 sizeworld = maxworld - minworld;

		maxworld = new Vector2(Screen.width, Screen.height) - sizeworld;

		float x = Mathf.Clamp(minworld.x, 0, maxworld.x);
		float y = Mathf.Clamp(minworld.y, 0, maxworld.y);

		Vector2 offset = (Vector2)transform.position - minworld;

		transform.position = new Vector2(x, y) + offset;
	}
    //--------------METHOD--------------------//
    //------작성자: 201202971 문지환----------//
}
