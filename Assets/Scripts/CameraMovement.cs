using Lean.Touch;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
	[Header("Movement Size")]
	[Tooltip("Minimum that camera can move")]
	public Vector2 Min;
	[Tooltip("Maximum that camera can move")]
	public Vector2 Max;
	
	[Space]

	private Camera Cam;

	[Tooltip("Ignore fingers with StartedOverGui?")]
	public bool IgnoreStartedOverGui = true;

	[Tooltip("Ignore fingers with IsOverGui?")]
	public bool IgnoreIsOverGui;

	[Tooltip("Ignore fingers if the finger count doesn't match? (0 = any)")]
	public int RequiredFingerCount;

	[Tooltip("The sensitivity of the movement, use -1 to invert")]
	public float Sensitivity = 1.0f;

	public LeanScreenDepth ScreenDepth;

	public UnityEvent OnEndScreen;

	public virtual void SnapToSelection()
	{
		var center = default(Vector3);
		var count = 0;

		for (var i = 0; i < LeanSelectable.Instances.Count; i++)
		{
			LeanSelectable selectable = LeanSelectable.Instances[i];

			if (selectable.IsSelected == true)
			{
				center += selectable.transform.position;
				count += 1;
			}
		}

		if (count > 0)
		{
			transform.position = center / count;
		}
	}

	protected virtual void Update()
	{
		// Get the fingers we want to use
		System.Collections.Generic.List<LeanFinger> fingers = LeanTouch.GetFingers(IgnoreStartedOverGui, IgnoreIsOverGui, RequiredFingerCount);

		// Get the last and current screen point of all fingers
		Vector2 lastScreenPoint = LeanGesture.GetLastScreenCenter(fingers);
		Vector2 screenPoint = LeanGesture.GetScreenCenter(fingers);

		// Get the world delta of them after conversion
		Vector3 worldDelta = ScreenDepth.ConvertDelta(lastScreenPoint, screenPoint, gameObject);
		worldDelta.y = 0;

		// Pan the camera based on the world delta with Min, Max value
		var newPosition = transform.position - (worldDelta * Sensitivity);
		
		if (newPosition.x < Min.x)
		{
			newPosition.x = Min.x;
		}

		if (newPosition.x > Max.x)
		{
			newPosition.x = Max.x;
			OnEndScreen.Invoke();
		}

		transform.position = newPosition;
	}
}
