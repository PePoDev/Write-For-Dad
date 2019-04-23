using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
	private RectTransform rect;
	private float fixPosX;

	private void Start()
	{
		rect = GetComponent<RectTransform>();
		fixPosX = rect.anchoredPosition.x;
	}

	public void OnDrag()
	{
		var newPos = Camera.main.ViewportToWorldPoint(Input.mousePosition);
		newPos.x = fixPosX;
		newPos.y -= 1500f;

		if (newPos.y > 30)
			return;

		rect.anchoredPosition = newPos;
	}
}
