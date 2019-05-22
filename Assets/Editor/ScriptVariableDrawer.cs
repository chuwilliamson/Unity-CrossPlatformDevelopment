using System.Linq;
using Chuwilliamson.Attributes;
using UnityEditor;
using UnityEngine;

namespace Attributes
{



    [CustomPropertyDrawer(typeof(ScriptVariableAttribute))]
    public class ScriptVariableDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = base.GetPropertyHeight(property, label);
            var extra = GUI.skin.button.CalcSize(label).y + EditorGUIUtility.standardVerticalSpacing;
            return ScriptVariableAttribute.Verbose ? height + extra : height;// if it's verbose give it extra height
        }

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var vars = ScriptVariableAttribute.Vars.Where(t => t.GetType() == fieldInfo.FieldType).ToList();
            var selected = vars.FindIndex(stat => property.objectReferenceValue == stat);
            var contents = vars.Select(n => new GUIContent(n.name)).ToArray();

            var bottomContent = new GUIContent(label.text);

            if (ScriptVariableAttribute.Verbose)
            {
                position.height -= GUI.skin.button.CalcSize(bottomContent).y + EditorGUIUtility.standardVerticalSpacing;
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property, label);
                GUI.enabled = true;
                position.y += EditorStyles.objectField.CalcSize(label).y + EditorGUIUtility.standardVerticalSpacing;
            }

            EditorGUI.BeginChangeCheck();


            selected = EditorGUI.Popup(position, bottomContent, selected, contents);

            if (!EditorGUI.EndChangeCheck()) return;
            Debug.Log(selected.ToString());
            Debug.Log(vars);
            property.objectReferenceValue = vars[selected];
            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.Update();
        }

        public ScriptVariableAttribute ScriptVariableAttribute
        {
            get { return (ScriptVariableAttribute)attribute; }
        }
    }
}