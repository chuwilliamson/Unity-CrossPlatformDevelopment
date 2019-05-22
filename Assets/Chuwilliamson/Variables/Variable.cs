using System;
using UnityEngine;
using UnityEngine.Events;

namespace Chuwilliamson.Variables
{
    public class Variable : ScriptableObject
    {
        public object data;
        [NonSerialized] public UnityEvent<Variable> OnValueChange;
    }
}