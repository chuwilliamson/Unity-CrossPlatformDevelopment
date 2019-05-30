using Chuwilliamson.ScriptableObjects;
using UnityEngine;

namespace Assessment
{
    /// <summary>
    /// Interact through this behaviour to update shared state from non-shared state
    /// </summary>
    public class PlayerDataBehaviour : MonoBehaviour
    {
        /// <summary>
        /// assign via inspector
        /// </summary>
        [SerializeField, Tooltip("Assign this in inspector")]
        private PlayerData _runtimeInstance;
        
        // Use this for initialization
        void Start()
        {
            _runtimeInstance = Instantiate(_runtimeInstance);
            _runtimeInstance.name = "PlayerData";
        }

        public void Save()
        {
            Global.Instance.Save(_runtimeInstance);
        }
        //create a list of player data
        //modify the contents of the list
        //rebuild that list at runtime
        public void Load()
        {
            var clone = Global.Instance.Load<PlayerData>(_runtimeInstance);
            _runtimeInstance = Instantiate(clone);
            _runtimeInstance.name = "PlayerData";
        }

        public void GainExperience(int amount)
        {
            _runtimeInstance.Experience += amount;
        }

        public void AddItem(Item item)
        {
            _runtimeInstance.AddItem(item);
        }

        public void RemoveItem(Item item)
        {
            _runtimeInstance.RemoveItem(item);
        }

        public void RemoveItem(object[] args)
        {
            var sender = args[0] as Item;
            if (sender == null) return;
            RemoveItem(sender);
        }
        public void RemoveLastItem()
        {
            _runtimeInstance.RemoveLastItem();
        }
    }
}
