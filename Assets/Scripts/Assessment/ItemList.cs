using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chuwilliamson.GameEventSystem;
using Chuwilliamson.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class ItemList : ScriptableObject, ICollection<Item>
{
    [SerializeField] private List<Item> value;
    public GameEvent onInventoryChanged;

    public List<Item> Value
    {
        get { return value; }
        set
        {
            this.value = value;
            onInventoryChanged.Raise(this);
        }
    }

    public IEnumerator<Item> GetEnumerator()
    {
        return Value.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable) Value).GetEnumerator();
    }

    public void Add(Item item)
    {
        Value.Add(item);
    }

    public void Clear()
    {
        Value.Clear();
    }

    public bool Contains(Item item)
    {
        return Value.Contains(item);
    }

    public void CopyTo(Item[] array, int arrayIndex)
    {
        Value.CopyTo(array, arrayIndex);
        onInventoryChanged.Raise(this);
    }

    public bool Remove(Item item)
    {
        return Value.Remove(item);
    }

    public int Count => Value.Count;

    public bool IsReadOnly => ((ICollection<Item>) Value).IsReadOnly;
}