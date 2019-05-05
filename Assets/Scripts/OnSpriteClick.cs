using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnSpriteClick : MonoBehaviour
{
    public UnityEvent OnMouseDown_UnityEvent;
    private void OnMouseDown()
    {
        OnMouseDown_UnityEvent.Invoke();
    }
}
