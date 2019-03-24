using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class DrawGameManager : MonoBehaviour
{
	public Draw DrawComponent;

	public RectTransform ProgressBus;
	public TextMeshProUGUI scoreUI;

	public float baseScore = 0;
	public float score = 0;

	public float busLength;
	public float timeToAnimateTextScore;

	public void OnStart()
	{
		DrawComponent.isStarted = true;
	}

	public void TimeUp()
	{
		score = Mathf.Clamp(score + baseScore, 0f, 100f);
		DrawComponent.isStarted = false;

		// Tween bus for animate score
		ProgressBus.DOSizeDelta(new Vector2(ProgressBus.rect.width + busLength, ProgressBus.rect.height) * (score / 100f), timeToAnimateTextScore);
		StartCoroutine(AnimateScoreText());
	}

	private IEnumerator AnimateScoreText()
	{
		float startingScore = 0;
		yield return new WaitWhile(() =>
		{
			scoreUI.SetText(Mathf.RoundToInt(startingScore).ToString());
			startingScore += (Time.deltaTime / timeToAnimateTextScore) * score;
			return startingScore < score;
		});
		scoreUI.SetText(score.ToString());
	}
}
