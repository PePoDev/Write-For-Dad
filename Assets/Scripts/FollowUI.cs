using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUI : MonoBehaviour
{
	public RectTransform targetUI;
	public Vector2 offset;

	private RectTransform rectTransform;

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	private void LateUpdate()
	{
		rectTransform.anchoredPosition = targetUI.anchoredPosition + offset;
	}
}
