using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMesh))]
public class DamagePopup : MonoBehaviour
{
    [SerializeField]
    float arrive_time;
    [SerializeField]
    float alpha_delay;
    float perSecond;
    float startTime;

    void Awake()
    {
        Destroy(gameObject, arrive_time);
        perSecond = 1.0f;
        startTime = Time.time + alpha_delay;
    }
	void Update ()
    {
        //Move Position(Direction : Up)
        transform.position += Vector3.up * Time.deltaTime;

        //TextMesh FadeOut
        if(Time.time >= startTime)
        {
            Color color = GetComponent<TextMesh>().color;
            color.a -= perSecond * Time.deltaTime;
            GetComponent<TextMesh>().color = color;
        }
	}
    /// <summary>
    /// 카메라의 옵션이 업데이트 되고 난 이후에 실행하기 위해 LateUpdate()로 실행
    /// </summary>
    void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
