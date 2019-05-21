using UnityEngine;

namespace Chuwilliamson.ScriptableObjects
{
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        [SerializeField] private Serialization.Item item;

        [SerializeField] private bool success;

        public Serialization.Item Value
        {
            get { return item; }
        }

        public override string ToString()
        {
            return string.Format("Item: {0}, GUID: {1}, ItemImage: {2}", item.Name, item.GUID, item);
        }

        private void OnEnable()
        {
            item.GUID = GetInstanceID();
            item.Name = name;

            var obj = GameData.Load(item, GetInstanceID().ToString());
            if (obj != null)
            {
                success = true;
                item = obj;
            }
        }

        private void OnDisable()
        {
            if (item != null) GameData.Save(item, item.GUID.ToString());
        }
    }
}