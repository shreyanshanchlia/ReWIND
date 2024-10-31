using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragFertilizer : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public RectTransform toDragObject;
    public bool returnOnDragCancel;

	public UnityEvent DropEvent;

	Vector3 newPosition;

	[ReadOnly] [SerializeField] bool mouseOverCheckpoint;

	public void OnBeginDrag(PointerEventData eventData)
	{
		toDragObject.transform.SetAsLastSibling();
	}

	public void OnDrag(PointerEventData eventData)
	{
		newPosition	= Camera.main.ScreenToWorldPoint(Input.mousePosition);
		newPosition.z = 0;
		toDragObject.position = newPosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		toDragObject.position = this.transform.parent.position;
		gameObectUnderMouse();
	}

	private void gameObectUnderMouse()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
		if (hit.collider != null)
		{
			DropEvent.Invoke();
		}
	}
}
