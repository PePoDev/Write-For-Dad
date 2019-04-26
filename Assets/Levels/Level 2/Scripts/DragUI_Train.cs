using UnityEngine;

public class DragUI_Train : MonoBehaviour
{
	public RectTransform rect2;
	public Vector3 offset;

	private RectTransform rect;
	private float fixPosY;

	private void Start()
	{
		rect = GetComponent<RectTransform>();
		fixPosY = rect.anchoredPosition.y;
	}

	public void OnDrag()
	{
		var newPos = Camera.main.ViewportToWorldPoint(Input.mousePosition);

		newPos.y = fixPosY;
		//newPos.x += 4100f;

		if (newPos.x < 6813)
		{
			newPos.x = 6813;
		}

		rect.anchoredPosition = newPos;
		rect2.anchoredPosition = newPos + offset;
	}
}
