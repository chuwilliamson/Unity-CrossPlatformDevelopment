using System.Collections.Generic;
using Chuwilliamson.GameEventSystem;
using UnityEngine;

namespace Chuwilliamson.ScriptableObjects
{
    [CreateAssetMenu]
    public class PlayerInventory : ScriptableObject
    {
        [SerializeField] public List<Serialization.Item> itemList;

        public GameEvent OnInventoryChanged;

        private List<Serialization.Item> ItemList
        {
            get { return itemList; }
            set
            {
                OnInventoryChanged.Raise(this);
                itemList = value;
            }
        }

        private void OnEnable()
        {
            var obj = GameData.Load(ItemList, GetInstanceID().ToString());

            if (obj == null || obj.Count <= 0)
                GameData.Save(ItemList, GetInstanceID().ToString());
            else
                ItemList = GameData.Load(ItemList, GetInstanceID().ToString());
        }

        private void OnDisable()
        {
            GameData.Save(ItemList, GetInstanceID().ToString());
        }

        public void AddItem(Serialization.Item item)
        {
            ItemList.Add(item);
        }

        public void AddItem(Item item)
        {
            ItemList.Add(item.Value);
        }

        public void RemoveItem(Serialization.Item item)
        {
            ItemList.Remove(item);
        }

        public void RemoveItem(Item item)
        {
            ItemList.Remove(item.Value);
        }
    }
}