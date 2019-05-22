using Chuwilliamson.Attributes;
using Chuwilliamson.ScriptableObjects;
using UnityEngine;

namespace Chuwilliamson
{
    public class ItemBehaviour : MonoBehaviour, IInteractable
    {
        public bool dirty;

        [SerializeField] [ScriptVariable(Verbose = true)]
        private Item item;

        public void Interact(Object interactor)
        {
            PickupItem(interactor);
        }

        public void PickupItem(Object inventory)
        {
            if (dirty)
                return;
            ((PlayerInventory) inventory).AddItem(item);
            dirty = true;
        }
    }
}