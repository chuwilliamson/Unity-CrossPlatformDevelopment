using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Items/KevlarVest")]
public class KevlarVest : Armor
{
    public override void Execute()
    {
        throw new NotImplementedException();
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        _itemID = GetHashCode();

        if (_itemName == "")
            _itemName = "KevlarVest";
        name = _itemName;
        DisplayName = _itemName;
        if (obj == null)
            return;
         
    }
}