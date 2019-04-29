using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeWhenComeInPostion : MonoBehaviour
{
	public AudioSource BGM;

	public float min;
	public float max;

	private Transform cameraTransform;
	private float avg;

	private void Start()
	{
		cameraTransform = Camera.main.transform;
		avg = max - min;
	}

	private void Update()
    {
		if (cameraTransform.position.x > min && cameraTransform.position.x < max)
		{
			var avgg = cameraTransform.position.x - min;
			BGM.volume = 0.3f - (avgg / avg) * 0.33f;
		}
    }
}
