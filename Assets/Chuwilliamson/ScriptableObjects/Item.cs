using UnityEngine;

namespace Chuwilliamson.ScriptableObjects
{
    [CreateAssetMenu, System.Serializable]
    public class Item : ScriptableObject
    {
        [SerializeField] private Serialization.Item item;
        
        public Serialization.Item Value
        {
            get { return item; }
            set { item = value; }
        }
        
        public override string ToString()
        {
            return string.Format("Item: {0}, GUID: {1}, ItemImage: {2}", item.name, item.guid, item);
        }
    }
}