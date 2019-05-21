using System.Collections.Generic;
using UnityEngine;

namespace Chuwilliamson.ScriptableObjects
{
    [CreateAssetMenu]
    public class PlayerInventory : ScriptableObject
    {
        [SerializeField] public List<Serialization.Item> itemList;

        private void OnEnable()
        {
            var obj = GameData.Load(itemList, GetInstanceID().ToString());

            if (obj == null || obj.Count <= 0)
                GameData.Save(itemList, GetInstanceID().ToString());
            else
                itemList = GameData.Load(itemList, GetInstanceID().ToString());
        }

        private void OnDisable()
        {
            GameData.Save(itemList, GetInstanceID().ToString());
        }

        public void AddItem(Serialization.Item item)
        {
            itemList.Add(item);
        }

        public void AddItem(Item item)
        {
            itemList.Add(item.Value);
        }

        public void RemoveItem(Serialization.Item item)
        {
            itemList.Remove(item);
        }

        public void RemoveItem(Item item)
        {
            itemList.Remove(item.Value);
        }
    }
}