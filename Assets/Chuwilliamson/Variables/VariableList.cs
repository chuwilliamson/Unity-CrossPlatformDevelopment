using System;
using System.Collections.Generic;
using Chuwilliamson.ScriptableObjects;
using UnityEngine;

namespace Chuwilliamson.Variables
{
    [CreateAssetMenu(menuName = "Variables/VariableList")]
    public class VariableList : ScriptableObject
    {
        [SerializeField] public Inventory inventory;

        private void OnEnable()
        {
            var inv = GameData.Load(inventory, GetInstanceID().ToString());
            if (inv == null || inv.Count > 0)
                GameData.Save(inventory, GetInstanceID().ToString());
            else
                GameData.Load(inventory, GetInstanceID().ToString());
        }

        private void OnDisable()
        {
            GameData.Save(inventory, GetInstanceID().ToString());
        }

    }
}