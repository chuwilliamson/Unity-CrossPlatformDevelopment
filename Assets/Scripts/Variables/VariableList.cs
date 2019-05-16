
using System.Collections.Generic;
using Serialization;
using UnityEngine;
using Variables;

[CreateAssetMenu(menuName = "Variables/VariableList")]
public class VariableList : ScriptableObject
{ 
    [System.Serializable]
    public class Inventory
    {
        public List<Variable> variables;
        public int Size
        {
            get { return variables.Count; }
        }
    }

    [SerializeField]
    public Inventory inventory;

    private void OnEnable()
    {
        var inv = GameData.Load(inventory);
        if (inv == null || inv.Size > 0)
            GameData.Save(inventory);
        else
        {
            GameData.Load(inventory);
        }
    }

    private void OnDisable()
    {
        GameData.Save(inventory);
    }
}