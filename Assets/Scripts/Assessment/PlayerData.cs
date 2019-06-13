using System.Collections.Generic;
using Chuwilliamson.ScriptableObjects;
using Chuwilliamson.Variables;
using UnityEngine;

namespace Assessment
{
    /// <summary>
    ///     Create a scriptable Object that will be
    ///     1. monobehaviour will instantiate an instance of a scriptableobject -> runtimeInstance
    ///     2. runtimeInstance will be modified by the user through interaction
    ///     3. runtimeInstance will be serialized to file on button press -> save
    ///     4. close the application
    ///     5. deserialize from file into runtimeInstance on button press -> load
    ///     Specifications:
    ///     Create a button for:
    ///     1. saving
    ///     2. loading
    ///     3. gaining experience
    ///     Create a label for:
    ///     1. Displaying Experience
    ///     2. Displaying Level
    /// </summary>
    [CreateAssetMenu]
    public class PlayerData : ScriptableObject
    {
        [Header("References")]
        [SerializeField] private IntVariable experienceRef;
        [SerializeField] private ItemList itemsRef;
        [SerializeField] private IntVariable levelRef;
        
        [Header("Local")]
        [SerializeField] private int experience;
        [SerializeField] private List<Item> itemList;
        [SerializeField] private int level;
        

        public int Experience
        {
            get { return experience; }
            set
            {
                experience = value;
                level = 1 + experience / 10;

                levelRef.Value = level;
                experienceRef.Value = experience;
            }
        }

        public int Level => level;

        public void AddItem(Item item)
        {
            itemList.Add(item);
            itemsRef.Value = itemList;
        }

        public void RemoveItem(Item item)
        {
            if (!itemList.Contains(item))
                return;
            itemList.Remove(item);
            itemsRef.Value = itemList;
        }

        public void RemoveLastItem()
        {
            RemoveItem(itemList[itemList.Count -1]);
        }

        private void OnEnable()
        {
            if (levelRef != null && experienceRef != null && levelRef != null) // we have properly instantiated a clone
            {
                Experience = experience;
                itemsRef.Value = itemList;
            }
        }

        
    }
}