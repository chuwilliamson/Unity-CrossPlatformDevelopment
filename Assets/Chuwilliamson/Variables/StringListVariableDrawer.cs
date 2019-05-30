#if UNITY_EDITOR
using UnityEditor;

namespace Chuwilliamson.Variables
{
    [CustomEditor(typeof(StringListVariable))]
    public class StringListVariableDrawer : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}
#endif