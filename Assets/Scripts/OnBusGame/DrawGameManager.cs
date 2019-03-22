using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGameManager : MonoBehaviour
{
	public Draw DrawComponent;

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

		print(score);
	}
}
