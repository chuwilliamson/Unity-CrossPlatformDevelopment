using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
#endif

namespace Chuwilliamson.Variables
{
    [CreateAssetMenu(menuName = "Variables/List/string")]
    public class StringListVariable : ScriptableObject
    {
        public List<string> value = new List<string>();
    }

#if UNITY_EDITOR
#endif
}