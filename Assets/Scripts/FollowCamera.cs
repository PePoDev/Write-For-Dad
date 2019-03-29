using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public Vector3 offset;
    public Vector3 EndPosition;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 Pos = cam.transform.position + offset;
        transform.position = Pos.x > EndPosition.x ? EndPosition : Pos;
    }
}
