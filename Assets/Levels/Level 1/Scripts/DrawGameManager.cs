using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class DrawGameManager : MonoBehaviour
{
    public Draw DrawComponent;
    public TimerController timerController;

    public RectTransform ProgressBus;
    public TextMeshProUGUI scoreUI;

    private Vector2 defaultBusSize;

    public float baseScore = 0;
    public float score = 0;

    public float busLength;
    public float timeToAnimateTextScore;

	public bool isPlaying = false;

	private void Start()
	{
		defaultBusSize = ProgressBus.sizeDelta;
	}

	public void OnStart()
    {
        DrawComponent.isStarted = true;
    }

    public void TimeUp()
    {
        score = Mathf.Clamp(score + baseScore, 0f, 100f);
        DrawComponent.isStarted = false;

        // Tween bus for animate score
        ProgressBus.DOSizeDelta(new Vector2(ProgressBus.rect.width + busLength, ProgressBus.rect.height), timeToAnimateTextScore);
        StartCoroutine(AnimateScoreText());
    }

    public void GameReset()
    {
        score = 0f;
        baseScore = 0f;

        foreach (Transform child in DrawComponent.GroupDrawObjects)
        {
            Destroy(child.gameObject);
        }

        timerController.StopTimer();
        timerController.ResetTimer();
        ProgressBus.parent.gameObject.SetActive(false);
        ProgressBus.sizeDelta = defaultBusSize;
    }

    private IEnumerator AnimateScoreText()
    {
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
