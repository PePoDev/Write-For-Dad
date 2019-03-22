using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
	public Image sign;
	public Sprite yellow, red;
	public TextMeshProUGUI TextToDisplay;
	public float TimeToCapture;

	public UnityEvent OnTimeUp;
	public UnityEvent OnStart;

	private float _timer;
	private bool isStarted = false;

	private void Start()
	{
		StartCoroutine(ReadyCountdown());
	}

	private void Update()
    {
        if (isStarted)
		{
			_timer += Time.deltaTime;

			if (TextToDisplay != null)
			{
				TextToDisplay.SetText(((int)(TimeToCapture - _timer)).ToString());
			}

			if (_timer > TimeToCapture)
			{
				_timer -= TimeToCapture;

				StopTimer();
				OnTimeUp.Invoke();
			}
		}
    }

	public void StartTimer()
	{
		isStarted = true;
		if (OnStart != null)
		{
			OnStart.Invoke();
		}
	}

	public void StopTimer()
	{
		isStarted = false;
	}

	IEnumerator ReadyCountdown()
	{
		yield return new WaitForSeconds(2f);
		sign.sprite = yellow;
		yield return new WaitForSeconds(2f);
		sign.sprite = red;

		StartTimer();
	}
}
