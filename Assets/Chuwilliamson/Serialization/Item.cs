using System;
using UnityEngine;

namespace Chuwilliamson.Serialization
{
    [Serializable]
    public class Item
    {
        public int GUID;
        public Sprite ItemImage;
        public string Name;

        public override string ToString()
        {
            return string.Format("Item: {0}, GUID: {1}, ItemImage: {2}", Name, GUID, ItemImage);
        }
    }
}