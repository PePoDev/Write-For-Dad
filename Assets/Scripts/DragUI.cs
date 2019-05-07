using Lean.Touch;
using UnityEngine;
using UnityEngine.Events;

public class DragUI : MonoBehaviour
{
	public Vector2 maxDragable;
	public Vector2 minDragable;

	[Tooltip("Ignore fingers with StartedOverGui?")]
	public bool IgnoreStartedOverGui = true;

	[Tooltip("Ignore fingers with IsOverGui?")]
	public bool IgnoreIsOverGui;

	[Tooltip("Ignore fingers if the finger count doesn't match? (0 = any)")]
	public int RequiredFingerCount;

	[Tooltip("Does translation require an object to be selected?")]
	public LeanSelectable RequiredSelectable;

	[Tooltip("The camera the translation will be calculated using (None = MainCamera)")]
	public Camera Camera;

	public UnityEvent OnDragEndAtLeft;

#if UNITY_EDITOR
	protected virtual void Reset()
	{
		Start();
	}
#endif

	protected virtual void Start()
	{
		if (RequiredSelectable == null)
		{
			RequiredSelectable = GetComponent<LeanSelectable>();
		}
	}

	protected virtual void Update()
	{
		// Get the fingers we want to use
		var fingers = LeanSelectable.GetFingers(IgnoreStartedOverGui, IgnoreIsOverGui, RequiredFingerCount, RequiredSelectable);

		// Calculate the screenDelta value based on these fingers
		var screenDelta = LeanGesture.GetScreenDelta(fingers);

		if (screenDelta != Vector2.zero)
		{
			// Perform the translation
			if (transform is RectTransform)
			{
				TranslateUI(screenDelta);
			}
			else
			{
				Translate(screenDelta);
			}
		}
	}

	protected virtual void TranslateUI(Vector2 screenDelta)
	{
		// Screen position of the transform
		var screenPoint = RectTransformUtility.WorldToScreenPoint(Camera, transform.position);

		// Add the deltaPosition
		screenPoint += screenDelta;

		// Convert back to world space
		var worldPoint = default(Vector3);

		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, screenPoint, Camera, out worldPoint) == true)
		{
			transform.position = worldPoint;

			var rectTransform = GetComponent<RectTransform>();
			var newPos = rectTransform.anchoredPosition;

			if (newPos.x > maxDragable.x)
			{
				newPos.x = maxDragable.x;
			}

			if (newPos.y > maxDragable.y)
			{
				newPos.y = maxDragable.y;
			}

			if (newPos.x < minDragable.x)
			{
				newPos.x = minDragable.x;
				OnDragEndAtLeft.Invoke();
			}

			if (newPos.y < minDragable.y)
			{
				newPos.y = minDragable.y;
			}

			rectTransform.anchoredPosition = newPos;
		}
	}

	protected virtual void Translate(Vector2 screenDelta)
	{
		// Make sure the camera exists
		var camera = LeanTouch.GetCamera(Camera, gameObject);

		if (camera != null)
		{
			// Screen position of the transform
			var screenPoint = camera.WorldToScreenPoint(transform.position);

			// Add the deltaPosition
			screenPoint += (Vector3)screenDelta;

			// Convert back to world space
			transform.position = camera.ScreenToWorldPoint(screenPoint);
		}
		else
		{
			Debug.LogError("Failed to find camera. Either tag your cameras MainCamera, or set one in this component.", this);
		}
	}
}
