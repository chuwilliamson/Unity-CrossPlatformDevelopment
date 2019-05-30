using System;
using UnityEngine;
using UnityEngine.Events;

namespace Chuwilliamson.Variables
{
    public class Variable : ScriptableObject
    {
        public object data;
        [NonSerialized] public VariableChangeEvent OnValueChange = new VariableChangeEvent();
    }

    [System.Serializable]
    public class VariableChangeEvent : UnityEvent<Variable> { }
}