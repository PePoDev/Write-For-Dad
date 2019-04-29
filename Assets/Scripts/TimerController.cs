using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Image sign;
    public Sprite green, yellow, red;
    public TextMeshProUGUI TextToDisplay;
    public float TimeToCapture;
    public bool isStarted = false;

    public UnityEvent OnTimeUp;
    public UnityEvent OnStart;

    private float _timer = 0;

    private void Update()
    {
        if (isStarted)
        {
            _timer += Time.deltaTime;

            if (TimeToCapture - _timer > 0.99f && TimeToCapture - _timer < 4f)
            {
                sign.sprite = yellow;
            }
            else if (TimeToCapture - _timer < 1f)
            {
                sign.sprite = red;
            }

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
        sign.sprite = green;

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
    
    public void ResetTimer()
    {
        _timer = 0;
		TextToDisplay.SetText(Mathf.RoundToInt(TimeToCapture).ToString());
		sign.sprite = green;
		
	}
}
