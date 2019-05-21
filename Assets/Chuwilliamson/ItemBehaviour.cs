using Chuwilliamson.ScriptableObjects;
using UnityEngine;

namespace Chuwilliamson
{
    public class ItemBehaviour : MonoBehaviour
    {
        [SerializeField] private Item item;

        private void OnTriggerEnter(Collider other)
        {
        }
    }
}