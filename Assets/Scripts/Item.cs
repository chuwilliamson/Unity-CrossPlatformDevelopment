using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        public Sprite ItemImage;
        public int GUID;
        public string Name;
    }
}