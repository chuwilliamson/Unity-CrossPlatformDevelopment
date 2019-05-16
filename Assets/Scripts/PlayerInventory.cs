using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInventory : ScriptableObject
{
    private List<ScriptableObjects.Item> _items = new List<Item>();

    public void AddItem(ScriptableObjects.Item item)
    {
        _items.Add(item);

    }

    public void RemoveItem(ScriptableObjects.Item item)
    {
        if (_items.Contains(item))
            _items.Remove(item);
    }
}
