using Chuwilliamson.Attributes;
using Chuwilliamson.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Chuwilliamson.CrossPlatformDevelopment
{
    public class UIInventoryBehaviour : MonoBehaviour
    {
        [SerializeField] [ScriptVariable(Verbose = true)]
        private PlayerInventory _inventoryRef;

        // Use this for initialization
        private void Start()
        {
            _inventoryRef = Resources.Load<PlayerInventory>("PlayerInventory");
            OnInventoryChanged(_inventoryRef);
        }

        public void OnInventoryChanged(PlayerInventory inventory)
        {
            for (var i = 0; i < transform.childCount; i++) Destroy(transform.GetChild(i).gameObject);
            foreach (var item in inventory.itemList)
            {
                var child = new GameObject();
                var img = child.AddComponent<Image>();
                img.sprite = item.itemImage;
            }
        }
    }
}