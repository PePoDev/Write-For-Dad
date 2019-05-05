using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using TMPro;
using UnityEngine;

public class L3P3_Manager : MonoBehaviour
{
	public TimerController timerController;

	public RectTransform ProgressBus;
	public TextMeshProUGUI scoreUI;

	public float busLength;
	public float timeToAnimateTextScore;

	public GameObject tutorialButtonObj;
	public GameObject tutorialDialogObj;

	public Transform RootScoreTrigger;
	public Transform BusTransform;

	public float score { get; set; } = 0;

	private TweenerCore<Vector2, Vector2, VectorOptions> tempTweening;
	private Vector2 defaultBusSize;
	private Vector3 StartedBusPosition;
	private Vector3 StartedBusRotation;

	private bool showedTutorial = false;

	private void Start()
	{
		StartedBusPosition = BusTransform.position;
		StartedBusRotation = BusTransform.eulerAngles;
		defaultBusSize = ProgressBus.sizeDelta;
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
		foreach (Transform item in RootScoreTrigger)
		{
			item.gameObject.SetActive(true);
		}

		if (tempTweening.IsPlaying())
		{
			tempTweening.Kill();
		}

		BusTransform.position = StartedBusPosition;
		BusTransform.eulerAngles = StartedBusRotation;

		timerController.ResetTimer();
		timerController.StartTimer();

		ProgressBus.sizeDelta = defaultBusSize;
		ProgressBus.parent.gameObject.SetActive(false);

		score = 0f;
	}
}
