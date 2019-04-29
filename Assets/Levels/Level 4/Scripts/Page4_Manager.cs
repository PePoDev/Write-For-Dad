using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page4_Manager : MonoBehaviour
{
	public Animator animator;
	public GameObject[] floors;

	public float LiftOpenDelay;

	private bool isOpen = false;

	public void OpenLift(int floorIdx)
	{
		if (isOpen)
		{
			CloseLift();
		}
		
		StartCoroutine(waitColseLift());
		IEnumerator waitColseLift()
		{
			if (isOpen)
			{
				yield return new WaitForSeconds(LiftOpenDelay);
			}

			foreach (var item in floors)
			{
				item.SetActive(false);
			}
			floors[floorIdx].SetActive(true);

			animator.SetTrigger("Open");
			isOpen = true;
		}
	}


	public void CloseLift()
	{
		animator.SetTrigger("Close");
	}
}
