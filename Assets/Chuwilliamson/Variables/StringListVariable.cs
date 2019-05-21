using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Chuwilliamson.Variables
{
#endif
    [CreateAssetMenu(menuName = "Variables/List/string")]
    public class StringListVariable : ScriptableObject
    {
        public List<string> Value = new List<string>();
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(StringListVariable))]
    public class StringListVariableDrawer : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
#endif
}