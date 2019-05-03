using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class BusController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public float duration;

	private Camera m_Camera;
	private bool m_isRight = false;

	private void Start()
	{
		m_Camera = Camera.main;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		m_isRight = true;
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (Input.touchCount > 1)
			return;

		if (m_isRight)
		{
			if (Physics.Raycast(m_Camera.ViewportPointToRay(m_Camera.ScreenToViewportPoint(Input.mousePosition)), out RaycastHit hit))
			{
				if (hit.collider.tag.Equals("point"))
				{
					Transform hitTransform = hit.collider.GetComponent<PointData>().transform;
					transform.DOMove(hitTransform.position, duration);
					transform.DORotate(hitTransform.rotation.eulerAngles, duration);
				}
				if (hit.collider.tag.Equals("block"))
				{
					m_isRight = false;
				}
			}
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		m_isRight = false;
	}
}
