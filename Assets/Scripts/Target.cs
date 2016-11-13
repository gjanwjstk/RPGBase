using UnityEngine;

public class Target : MonoBehaviour
{
    //--------------FIELD---------------------//
    float start_pos;
    float end_pos;
    bool is_up;
    //------------EVENTMETHOD-----------------//
    void Awake()
    {
        start_pos = 1.5f;
        end_pos = 1.6f;
        is_up = true;

        gameObject.SetActive(false);
    }
    void Update()
    {
        transform.Rotate(Vector3.up * 100 * Time.deltaTime);
        Vector3 pos = transform.position;
        if (is_up)
        {
            pos.y += .00f;
            if (transform.position.y >= end_pos) is_up = false;
        }
        else
        {
            pos.y -= .001f;
            if (transform.position.y <= start_pos) is_up = true;
        }
        transform.position = pos;
    }
    //--------------METHOD--------------------//
    public void Target_On(Vector3 _pos)
    {
        _pos.y = start_pos;
        transform.position = _pos;
        gameObject.SetActive(true);
    }
	public void Target_Off()
    {
        gameObject.SetActive(false);
    }
    //------작성자: 201202971 문지환----------//
}
