using UnityEngine;
using UnityEngine.EventSystems;

namespace CrossPlatformDevelopment
{
    public class DemoPointerBehaviour : MonoBehaviour, IPointerClickHandler
    {
        public Object scriptableVariable;

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(gameObject.name);
        }
    }
}