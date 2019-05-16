using System;
using UnityEngine;
using UnityEngine.Events;
using Object = System.Object;

namespace Variables
{
    public class Variable : ScriptableObject
    {
        [NonSerialized]
        public UnityEvent<Variable> onValueChange;

        public object data;
    }
}