using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawGameManager : MonoBehaviour
{
	public Draw DrawComponent;

	public TextMeshProUGUI scoreUI;

	public int baseScore = 0;
	public int score = 0;

	public void OnStart()
	{
		DrawComponent.isStarted = true;
	}

	public void TimeUp()
	{
		score += baseScore;
		DrawComponent.isStarted = false;

		scoreUI.SetText(score.ToString());
	}
}
