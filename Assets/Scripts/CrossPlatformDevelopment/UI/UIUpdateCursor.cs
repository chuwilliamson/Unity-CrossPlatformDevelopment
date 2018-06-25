using UnityEngine;
using UnityEngine.EventSystems;

public class UIUpdateCursor : UIBehaviour
{
    public GameObject current;
    private RectTransform rectTransform;

    public GameObject Current
    {
        get { return current; }
        set
        {
            current = value;
            ResetPosition();
        }
    }

    protected override void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != current)
            Current = EventSystem.current.currentSelectedGameObject;
    }

    private void ResetPosition()
    {
        rectTransform.SetParent(current.transform);
        var rect = current.GetComponent<RectTransform>().rect;
        rectTransform.localPosition = new Vector3(rect.xMin, rect.yMin / rect.height, 0);
        rectTransform.SetAsLastSibling();
    }
}