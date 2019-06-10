using UnityEditor;
using UnityEngine;

namespace Assessment
{
    [CustomEditor(typeof(Global))]
    public class GlobalInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Execute a Function"))
            {
                var mt = (Global) target;
                mt.Response.Invoke();
            }
        }
    }
}