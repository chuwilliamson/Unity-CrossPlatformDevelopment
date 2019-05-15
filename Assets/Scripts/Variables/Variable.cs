using System;
using UnityEngine;
using UnityEngine.Events;

namespace Variables
{
    public class Variable : ScriptableObject
    {
        [NonSerialized]
        public UnityEvent<Variable> onValueChange;
    }
}