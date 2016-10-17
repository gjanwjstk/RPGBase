using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIShowToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField]
	GameObject tooltip_prefab;
	[TextArea(1, 30)]
	public string _text = "";

	GameObject current;

	void CreateToolTip()
	{
		current = (GameObject)Instantiate(tooltip_prefab, transform.position, Quaternion.identity);

		current.transform.SetParent(transform.root, true);
		current.transform.SetAsLastSibling(); //자식들 중에 제일 마지막 자식으로 설정해 주세요
		//UI에서 제일 마지막 자식은 맨 앞에 보이기 때문에.

		current.GetComponentInChildren<Text>().text = _text;
	}
	void DestroyToolTip()
	{
		CancelInvoke("CreateToolTip");

		Destroy(current);
	}
	public void OnPointerEnter(PointerEventData d) { Invoke("CreateToolTip", 0.5f); }
	public void OnPointerExit(PointerEventData d) { DestroyToolTip(); }
	void OnDisable() { DestroyToolTip(); }
	void OnDestroy() { DestroyToolTip(); }
}
