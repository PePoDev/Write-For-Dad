using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIElementDragger : MonoBehaviour
{
    public const string DRAGGABLE_TAG = "UIDraggable";
    public const string TARGET_TAG = "Target";

    public GameObject kAll;
    public AudioSource audio_paste;

    private bool dragging = false;
	
    private Vector2 originalPosition;
    private Transform objectToDrag;
    private Image objectToDragImage;
    private int count = 0;
    private List<RaycastResult> hitObjects = new List<RaycastResult>();

    #region Monobehaviour API

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            objectToDrag = GetDraggableTransformUnderMouse();

            if (objectToDrag != null)
            {
                dragging = true;

                objectToDrag.SetAsLastSibling();

                originalPosition = objectToDrag.position;
                objectToDragImage = objectToDrag.GetComponent<Image>();
                objectToDragImage.raycastTarget = false;
            }
        }

        if (dragging)
        {
            objectToDrag.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (objectToDrag != null)
            {
                Transform objectToReplace = GetTargetTransformUnderMouse();

                if (objectToReplace != null && objectToDrag.name.Equals(objectToReplace.name))
                {
                    objectToDrag.gameObject.SetActive(false);
                    objectToReplace.GetComponent<Image>().color = Color.white;
                    audio_paste.Play();
                    count++;

                    if (count == 4)
                    {
                        kAll.SetActive(true);
                    }
                }

                objectToDragImage.raycastTarget = true;
                objectToDrag = null;
            }

            dragging = false;
        }
    }

    private GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;

        return hitObjects.First().gameObject;
    }
    private Transform GetDraggableTransformUnderMouse()
    {
        GameObject clickedObject = GetObjectUnderMouse();

        // get top level object hit
        if (clickedObject != null && clickedObject.tag == DRAGGABLE_TAG)
        {
            return clickedObject.transform;
        }

        return null;
    }
    private Transform GetTargetTransformUnderMouse()
    {
        GameObject clickedObject = GetObjectUnderMouse();

        // get top level object hit
        if (clickedObject != null && clickedObject.tag == TARGET_TAG)
        {
            return clickedObject.transform;
        }

        return null;
    }
    #endregion
}