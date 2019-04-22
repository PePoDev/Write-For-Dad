using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Level2_Manager : MonoBehaviour
{
	#region Variables
	public TimerController timerController;

	public RectTransform ProgressBus;
	public TextMeshProUGUI scoreUI;

	public Transform tutorialDialogObj;
	public Transform tutorialButtonObj;

	public RectTransform[] transformObjs;

	public float scaleSwipeMultiplier;
	public float busLength;
	public float timeToAnimateTextScore;

	private Vector2[] positionObjs = new Vector2[5];
	private Vector2 defaultBusSize;

	private int[] pushCount = new int[5];
	private float score;
	private bool showedTutorial = false;
	private bool hasStarted = false;
	private bool isPlaying = false;
	#endregion

	#region Main Methods
	private void Start()
	{
		defaultBusSize = ProgressBus.sizeDelta;

		for (int i = 0; i < transformObjs.Length; i++)
		{
			positionObjs[i] = transformObjs[i].anchoredPosition;
		}
	}

	private void Update()
	{
		if ((!showedTutorial && isPlaying) && !hasStarted)
		{
			if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
			{
				timerController.StartTimer();
				hasStarted = true;
			}
		}
	}
	#endregion

	#region Helper Method
	public void ShowTutorial()
	{
		if (showedTutorial == false)
		{
			tutorialDialogObj.gameObject.SetActive(true);
			showedTutorial = true;
		}
		else
		{
			tutorialDialogObj.gameObject.SetActive(false);
			tutorialButtonObj.gameObject.SetActive(false);
			isPlaying = true;
			showedTutorial = false;
		}
	}

	public void Swipe(RectTransform obj)
	{
		if (isPlaying)
		{
			switch (obj.gameObject.name)
			{
				case "Obj (1)":
					if (pushCount[0] == 3)
					{
						obj.anchoredPosition += Vector2.left * scaleSwipeMultiplier * 10f;
					}
					else if (pushCount[0] > 3)
					{
						return;
					}
					obj.anchoredPosition += Vector2.left * scaleSwipeMultiplier;
					pushCount[0]++;
					break;
				case "Obj (2)":
					if (pushCount[1] == 3)
					{
						obj.anchoredPosition += Vector2.left * scaleSwipeMultiplier * 10f;
					}
					else if (pushCount[1] > 3)
					{
						return;
					}
					obj.anchoredPosition += Vector2.left * scaleSwipeMultiplier;
					pushCount[1]++;
					break;
				case "Obj (3)":
					if (pushCount[2] == 3)
					{
						obj.anchoredPosition += Vector2.up * scaleSwipeMultiplier * 10f;
					}
					else if (pushCount[2] > 3)
					{
						return;
					}
					obj.anchoredPosition += Vector2.up * scaleSwipeMultiplier;
					pushCount[2]++;
					break;
				case "Obj (4)":
					if (pushCount[3] == 3)
					{
						obj.anchoredPosition += Vector2.right * scaleSwipeMultiplier * 10f;
					}
					else if (pushCount[3] > 3)
					{
						return;
					}
					obj.anchoredPosition += Vector2.right * scaleSwipeMultiplier / 2f;
					pushCount[3]++;
					break;
				case "Obj (5)":
					if (pushCount[4] == 3)
					{
						obj.anchoredPosition += Vector2.down * scaleSwipeMultiplier * 10f;
					}
					else if (pushCount[4] > 3)
					{
						return;
					}
					obj.anchoredPosition += Vector2.down * scaleSwipeMultiplier / 3f;
					pushCount[4]++;
					break;
			}
		}
	}

	public void GameEnd()
	{
		score = GetScore();

		ProgressBus.parent.gameObject.SetActive(true);
		StartCoroutine(AnimateScoreText());
		IEnumerator AnimateScoreText()
		{
			ProgressBus.DOSizeDelta(new Vector2(ProgressBus.rect.width + busLength, ProgressBus.rect.height), timeToAnimateTextScore);
			float startingScore = 0;
			yield return new WaitWhile(() =>
			{
				scoreUI.SetText(Mathf.Clamp(Mathf.RoundToInt(startingScore), 0, 100).ToString());
				startingScore += (Time.deltaTime / timeToAnimateTextScore) * score;
				return startingScore < score;
			});
			scoreUI.SetText(Mathf.RoundToInt(score).ToString());
		}
	}

	private float GetScore()
	{
		float _score = 0f;
		int[] set = new int[4];

		for (int i = 0; i < pushCount.Length; i++)
		{
			print(pushCount[i]);
			set[pushCount[i] - 1]++;
		}

		if (set[3] > 0)
		{
			return 100 - ((5 - set[3]) * 10);
		}

		if (set[2] > 0)
		{
			return 100 - ((4 - set[3]) * 10);
		}

		if (set[1] > 0)
		{
			return 100 - ((4 - set[3]) * 10);
		}

		if (set[0] > 0)
		{
			return 100 - ((4 - set[3]) * 10);
		}
		return _score;
	}

	public void GameReset()
	{
		timerController.StopTimer();
		timerController.ResetTimer();
		pushCount = new int[5];
		score = 0f;
		hasStarted = false;
		isPlaying = false;
		for (int i = 0; i < transformObjs.Length; i++)
		{
			transformObjs[i].anchoredPosition = positionObjs[i];
		}
	}
	#endregion
}