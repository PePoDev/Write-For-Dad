using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Level4 : MonoBehaviour
{
	#region Variables
	public TimerController timerController;

	public AudioSource Shot, Reload;
	public AudioSource AudioScore, AudioBusScore;

	public RectTransform ProgressBus;
	public TextMeshProUGUI scoreUI;

	public Transform tutorialDialogObj;
	public Transform tutorialButtonObj;

	public GameObject[] Games;

	public float busLength;
	public float timeToAnimateTextScore;

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
	}
	public void TimeUp()
	{
		Reload.Play();
		if (choiceSelectedIndex == listOfCorrectAnswer[questionNumber])
		{
			correctAnswer++;
			Games[questionNumber].SetActive(false);
		}

		questionNumber++;

		if (listOfCorrectAnswer.Length == questionNumber)
		{
			GameEnd();
		}
		else
		{
			Games[questionNumber].SetActive(true);
			timerController.ResetTimer();
			timerController.StartTimer();
		}
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
		AudioScore.Play();
		AudioBusScore.Play();
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
	public void GameReset()
	{
		timerController.ResetTimer();
		timerController.StartTimer();

		ProgressBus.sizeDelta = defaultBusSize;

		questionNumber = 0;
		choiceSelectedIndex = 0;
		correctAnswer = 0;
		score = 0f;
	}
	#endregion
}