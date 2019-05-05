using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderEvent : MonoBehaviour
{
	public UnityEvent OnColliderEnter;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		OnColliderEnter.Invoke();
	}
}
