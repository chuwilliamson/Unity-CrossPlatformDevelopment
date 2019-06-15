using Chuwilliamson.Attributes;
using Chuwilliamson.ScriptableObjects;
using UnityEngine;

namespace Chuwilliamson
{
    public class ItemBehaviour : MonoBehaviour
    {
        [SerializeField, ScriptVariable(Verbose = true)]
        private Item item;
    }
}