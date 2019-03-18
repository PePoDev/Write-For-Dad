using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGameManager : MonoBehaviour
{
	public int baseScore = 0;
	private int score = 0;

	public void TimeUp()
	{
		score += baseScore;

	}
}
