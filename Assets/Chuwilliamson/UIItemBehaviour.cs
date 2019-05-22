using Chuwilliamson.Attributes;
using Chuwilliamson.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Chuwilliamson
{
    public class UIItemBehaviour : MonoBehaviour
    {
        [SerializeField] private Image _image;

        [SerializeField] [ScriptVariable(Verbose = true)]
        private Item
            _item;

        // Use this for initialization
        private void Start()
        {
            if (_image != null)
                _image = GetComponent<Image>();

            if (_item != null)
                _image.sprite = _item.Value.itemImage;
        }
    }
}