using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Level4 : MonoBehaviour
{
	#region Variables
	public TimerController timerController;

	public AudioSource Shot, Reload;

	public RectTransform ProgressBus;
	public TextMeshProUGUI scoreUI;

	public Transform tutorialDialogObj;
	public Transform tutorialButtonObj;

	public GameObject[] Games;

	public float busLength;
	public float timeToAnimateTextScore;

	private TweenerCore<Vector2, Vector2, VectorOptions> tempTweening;
	private Vector2 defaultBusSize;

	private int questionNumber = 0;
	private int choiceSelectedIndex = 0;
	private int correctAnswer = 0;
	private int[] listOfCorrectAnswer = { 3, 5, 2, 4, 1, 6, 3 };
	private float score = 0f;
	private bool showedTutorial = false;
	#endregion

	#region Main Methods
	private void Start()
	{
		defaultBusSize = ProgressBus.sizeDelta;
	}
	#endregion

	#region Helper Method
	public void Answer(int item)
	{
		choiceSelectedIndex = item;
		Shot.Play();
		Reload.Play();

		correctAnswer += choiceSelectedIndex == listOfCorrectAnswer[questionNumber] ? 1 : 0;

		Games[questionNumber].SetActive(false);
		questionNumber++;

		if (listOfCorrectAnswer.Length == questionNumber)
		{
			timerController.StopTimer();
			timerController.OnTimeUp.Invoke();
			return;
		}

		Games[questionNumber].SetActive(true);
	}
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
			showedTutorial = false;
			timerController.StartTimer();
		}
	}
	public void GameEnd()
	{
		score = correctAnswer == 0 ? 0 : 30 + (correctAnswer * 10);

		StartCoroutine(AnimateScoreText());
		IEnumerator AnimateScoreText()
		{
			var progressScore = busLength * (score / 100f);
			tempTweening = ProgressBus.DOSizeDelta(new Vector2(ProgressBus.rect.width + progressScore, ProgressBus.rect.height), timeToAnimateTextScore);
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
	public void GameReset()
	{
		if (tempTweening.IsPlaying())
		{
			tempTweening.Kill();
		}

		timerController.ResetTimer();
		timerController.StartTimer();

		ProgressBus.sizeDelta = defaultBusSize;
		ProgressBus.parent.gameObject.SetActive(false);

		Games[0].SetActive(true);
		for (int i = 1; i < Games.Length; i++)
		{
			Games[i].SetActive(false);
		}

		questionNumber = 0;
		choiceSelectedIndex = 0;
		correctAnswer = 0;
		score = 0f;
	}
	#endregion
}