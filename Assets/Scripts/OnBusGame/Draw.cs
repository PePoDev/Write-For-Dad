using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draw : MonoBehaviour
{
	public GameObject[] trailBrushPrefab;
	public GameObject[] paintPrefab;

	public int selectedIndex { get; set; } = -1;
	public bool isBrush { get; set; } = false;
	public bool isStarted { get; set; } = false;
	public bool CanDrawOnPanel { get; set; } = false;

	[SerializeField] private DrawGameManager gameManager;
	private GameObject currentTrail;
	private Vector3 startPosition;
	private Plane objPlane;
	private Camera Cam;
	private Ray mRay;

	private int currentOrder = 0;

	private void Start()
	{
		Cam = Camera.main;
		objPlane = new Plane(Camera.main.transform.forward * -1, transform.position);
	}

	private void Update()
	{
		if (isStarted == false || (selectedIndex < 0 || !CanDrawOnPanel))
			return;

		if (isBrush)
		{
			if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
			{
				mRay = Cam.ScreenPointToRay(Input.mousePosition);

				float rayDistance;
				if (objPlane.Raycast(mRay, out rayDistance))
				{
					startPosition = mRay.GetPoint(rayDistance);
				}

				currentTrail = Instantiate(trailBrushPrefab[selectedIndex], startPosition, Quaternion.identity);
				currentTrail.GetComponent<TrailRenderer>().sortingOrder = currentOrder++;

				switch (selectedIndex)
				{
					case 0:
						gameManager.baseScore = gameManager.baseScore < 10 ? 10 : gameManager.baseScore;
						break;
					case 1:
					case 3:
						gameManager.baseScore = gameManager.baseScore < 40 ? 40 : gameManager.baseScore;
						break;
					case 2:
						gameManager.baseScore = gameManager.baseScore < 70 ? 70 : gameManager.baseScore;
						break;
				}
			}
			else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0))
			{
				if (currentTrail == null)
					return;

				mRay = Cam.ScreenPointToRay(Input.mousePosition);
				float rayDistance;
				if (objPlane.Raycast(mRay, out rayDistance))
				{
					currentTrail.transform.position = mRay.GetPoint(rayDistance);
				}

				gameManager.score += (int)(30f * Time.deltaTime);
			}
			else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
			{
				if (currentTrail == null)
					return;

				if (Vector3.Distance(currentTrail.transform.position, startPosition) < 0.1f)
				{
					Destroy(currentTrail);
				}
			}
		}
		else
		{
			switch (selectedIndex)
			{
				case 1:
					gameManager.baseScore = gameManager.baseScore < 10 ? 10 : gameManager.baseScore;
					break;
				case 0:
					gameManager.baseScore = gameManager.baseScore < 40 ? 10 : gameManager.baseScore;
					break;
				case 2:
					gameManager.baseScore = gameManager.baseScore < 70 ? 40 : gameManager.baseScore;
					break;
			}
		}

	}
}
