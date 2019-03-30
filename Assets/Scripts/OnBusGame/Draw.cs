using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draw : MonoBehaviour
{
    public DrawGameManager gameManager;
    public GameObject[] trailBrushPrefab;
    public GameObject[] paintPrefab;
    public AudioSource audioTimer;
    public AudioSource[] audioBrushs;
    public RectTransform mainCanvas;
    public Transform GroupDrawObjects;

    public int selectedIndex { get; set; } = -1;
    public bool isBrush { get; set; } = false;
    public bool isStarted { get; set; } = false;
    public bool CanDrawOnPanel { get; set; } = false;

    private GameObject currentTrail;
    private Vector3 startPosition;
    private Plane objPlane;
    private Camera Cam;
    private Ray mRay;

    private int currentOrder = 1;

    private void Start()
    {
        Cam = Camera.main;
        objPlane = new Plane(Camera.main.transform.forward * -1, transform.position);
    }

    private void Update()
    {
        if (selectedIndex < 0 || !CanDrawOnPanel)
            return;

        if (isBrush)
        {
            // On touch or click began
            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
            {
                if (gameManager.timerController.isStarted == false)
                {
                    gameManager.timerController.StartTimer();
                    audioTimer.Play();
                }

                if (isStarted == false)
                {
                    return;
                }

                mRay = Cam.ScreenPointToRay(Input.mousePosition);

                float rayDistance;
                if (objPlane.Raycast(mRay, out rayDistance))
                {
                    startPosition = mRay.GetPoint(rayDistance);
                }

                currentTrail = Instantiate(trailBrushPrefab[selectedIndex], startPosition, Quaternion.identity, GroupDrawObjects);
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

                audioBrushs[selectedIndex + (isBrush ? 0 : trailBrushPrefab.Length)].Play();
            }

            // On touch or click moved
            else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0))
            {
                if (currentTrail == null)
                {
                    return;
                }

                mRay = Cam.ScreenPointToRay(Input.mousePosition);
                float rayDistance;
                if (objPlane.Raycast(mRay, out rayDistance))
                {
                    currentTrail.transform.position = mRay.GetPoint(rayDistance);
                }

                gameManager.score += 30f / 20f * Time.deltaTime;

                if (audioBrushs[selectedIndex + (isBrush ? 0 : trailBrushPrefab.Length)].isPlaying == false)
                {
                    audioBrushs[selectedIndex + (isBrush ? 0 : trailBrushPrefab.Length)].Play();
                }
            }

            // On touch or click end
            else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
            {
                if (currentTrail == null)
                    return;

                if (Vector3.Distance(currentTrail.transform.position, startPosition) < 0.1f)
                {
                    Destroy(currentTrail);
                }

                audioBrushs[selectedIndex + (isBrush ? 0 : trailBrushPrefab.Length)].Pause();
            }
        }
        else
        {
            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
            {
                audioBrushs[selectedIndex + (isBrush ? 0 : trailBrushPrefab.Length)].Play();

                if (gameManager.timerController.isStarted == false)
                {
                    audioTimer.Play();
                    gameManager.timerController.StartTimer();
                }

                if (isStarted == false)
                {
                    return;
                }
                Vector3 screenPos = Cam.ScreenToWorldPoint(Input.mousePosition);
                SpriteRenderer obj = Instantiate(
                    paintPrefab[selectedIndex].GetComponent<SpriteRenderer>(),
                    new Vector2(screenPos.x, screenPos.y),
                    Quaternion.identity,
                    GroupDrawObjects);
                obj.sortingOrder = currentOrder++;

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

                gameManager.score += 2;
            }
        }
    }
}
