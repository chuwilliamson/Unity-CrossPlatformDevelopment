using UnityEngine;
using UnityEngine.EventSystems;

public class UIDraggable : UIBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        var rt = GetComponent<RectTransform>();
        rt.transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }
}