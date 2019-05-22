using System;
using UnityEngine;

namespace Chuwilliamson.Serialization
{
    [Serializable]
    public class Item
    {
        public int guid;
        public Sprite itemImage;
        public string name;

        public override string ToString()
        {
            return string.Format("Item: {0}, GUID: {1}, ItemImage: {2}", name, guid, itemImage);
        }
    }
}