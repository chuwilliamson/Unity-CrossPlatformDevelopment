using Chuwilliamson.ScriptableObjects;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    private Editor prefabEditor;
    private Texture2D texture;
    private Editor textureEditor;
    private GameObject prefab;

    private void OnEnable()
    {
        var mt = target as Item;

        textureEditor = CreateEditor(mt.Value.itemImage.texture);
        prefabEditor = CreateEditor(serializedObject.FindProperty("prefab").objectReferenceValue);
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        var mt = target as Item;
        var prefabProperty = serializedObject.FindProperty("prefab");
        var valueProperty = serializedObject.FindProperty("item").FindPropertyRelative("itemImage");

        EditorGUILayout.PropertyField(valueProperty);
        var textureRect = GUILayoutUtility.GetRect(250, 250);
        if (valueProperty.objectReferenceValue != null)
            textureEditor.OnPreviewGUI(textureRect, GUIStyle.none);

        EditorGUILayout.PropertyField(prefabProperty);
        var prefabRect = GUILayoutUtility.GetRect(250, 250);
        prefabRect.y += 25;
        prefabEditor.OnPreviewGUI(prefabRect, EditorStyles.whiteLabel);


        if (EditorGUI.EndChangeCheck())
        {
            textureEditor = CreateEditor(mt.Value.itemImage.texture);
            prefabEditor = CreateEditor(prefabProperty.objectReferenceValue);
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

    }
}