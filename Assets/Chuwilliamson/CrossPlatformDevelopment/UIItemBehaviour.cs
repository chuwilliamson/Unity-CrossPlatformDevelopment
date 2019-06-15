using Chuwilliamson.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Chuwilliamson
{
    public class UIItemBehaviour : MonoBehaviour
    {
        private Image image;

        [SerializeField] private Item item;

        // Use this for initialization
        private void Start()
        {
            image = GetComponent<Image>();

            if (item != null)
                image.sprite = item.Value.itemImage;
        }
    }
}