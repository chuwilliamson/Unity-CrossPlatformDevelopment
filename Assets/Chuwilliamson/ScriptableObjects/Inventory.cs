using System.Collections;
using System.Collections.Generic;
using Chuwilliamson.GameEventSystem;
using UnityEngine;

namespace Chuwilliamson.ScriptableObjects
{
    [CreateAssetMenu]
    public class Inventory : ScriptableObject, IList<Serialization.Item>
    {
        [SerializeField] public List<Serialization.Item> itemList;

        public GameEvent onInventoryChanged;
 
        private List<Serialization.Item> ItemList
        {
            get { return itemList; }
            set
            {
                onInventoryChanged.Raise(this);
                itemList = value;
            }
        }
  
        public void Add(Item item)
        {
            ItemList.Add(item.Value);
        }


        public void Remove(Item item)
        {
            ItemList.Remove(item.Value);
        }

        public IEnumerator<Serialization.Item> GetEnumerator()
        {
            return itemList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) itemList).GetEnumerator();
        }

        public void Add(Serialization.Item item)
        {
            itemList.Add(item);
        }

        public void Clear()
        {
            itemList.Clear();
        }

        public bool Contains(Serialization.Item item)
        {
            return itemList.Contains(item);
        }

        public void CopyTo(Serialization.Item[] array, int arrayIndex)
        {
            itemList.CopyTo(array, arrayIndex);
        }

        public bool Remove(Serialization.Item item)
        {
            return itemList.Remove(item);
        }

        public int Count => itemList.Count;

        public bool IsReadOnly => false;

        public int IndexOf(Serialization.Item item)
        {
            return itemList.IndexOf(item);
        }

        public void Insert(int index, Serialization.Item item)
        {
            itemList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            itemList.RemoveAt(index);
        }

        public Serialization.Item this[int index]
        {
            get { return itemList[index]; }
            set { itemList[index] = value; }
        }
    }
}