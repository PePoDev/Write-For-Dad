using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDrawOnPanel : MonoBehaviour
{
	public Draw draw;

	private void OnMouseEnter()
	{
		draw.CanDrawOnPanel = true;
	}

	private void OnMouseExit()
	{
		draw.CanDrawOnPanel = false;
	}
}
