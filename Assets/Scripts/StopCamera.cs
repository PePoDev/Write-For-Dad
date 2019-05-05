using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StopCamera : MonoBehaviour
{
	public UnityEvent OnTick;
	public float[] trickList;

	private int currentTrick = 0;
	private Camera cam;

	private void Start()
	{
		cam = Camera.main;
	}

	private void Update()
	{
		if (currentTrick < trickList.Length && cam.transform.position.x > trickList[currentTrick])
		{
			cam.transform.position = new Vector3(trickList[currentTrick], cam.transform.position.y, cam.transform.position.z);
			currentTrick++;
			OnTick.Invoke();
		}
	}
}
