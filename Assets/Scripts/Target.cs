using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    float start_pos;
    float end_pos;
    bool is_up;

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
}
