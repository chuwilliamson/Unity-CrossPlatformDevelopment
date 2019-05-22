using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace Chuwilliamson.ScriptableObjects
{
    public class GameDataWindow : EditorWindow
    {
        public string _lookup;
        private FieldInfo _field;

        [MenuItem("Tools/GameData")]
        private static void Init()
        {
            var window = (GameDataWindow)GetWindow(typeof(GameDataWindow));
            window.Show();
        }

        public static void Save(string fileName, object data)
        {
            Debug.Log("saving data");
            var path = Path.Combine(Application.streamingAssetsPath, fileName);
            if (data == null)
                return;
            
            File.WriteAllText(path, JsonUtility.ToJson(data));
        }

        public static T Load<T>(string fileName) where T : ScriptableObject
        {
            
            var path = Path.Combine(Application.streamingAssetsPath, fileName);
            var json = File.ReadAllText(path);
            var obj = CreateInstance<T>();
            JsonUtility.FromJsonOverwrite(json, obj);
            return obj;
        }


        private void OnGUI()
        {
            
            
            _lookup = EditorGUILayout.DelayedTextField("lookup ", _lookup);
            if (GUILayout.Button("Save")) Save("GameSettings.json", this);

            if (GUILayout.Button("Load"))
            {
                var obj = Load<GameDataWindow>("GameSettings.json");
                this._lookup = obj._lookup;
            }

            DrawDictionary(new SerializedObject(GameData.Instance), _lookup);
        }

        public bool DrawDictionary(SerializedObject so, string propertyName)
        {
            if (propertyName == null)
                return false;
            _field = so.targetObject.GetType().GetField(propertyName);

            if (_field != null)
            {
                var fieldName = _field.Name;
                var fieldValue = _field.GetValue(so.targetObject) as Dictionary<string, string>;
                var gcName = new GUIContent(fieldName);
                EditorGUILayout.LabelField(gcName, GUILayout.Width(100));
                foreach (var kvp in fieldValue)
                {
                    EditorGUI.indentLevel++;
                    var gcKey = new GUIContent("Key: " + kvp.Key);
                    var gcValue = new GUIContent("Value: " + kvp.Value);
                    EditorGUILayout.BeginHorizontal();
                    
                    EditorGUILayout.LabelField(gcKey, GUILayout.Width(100));
                    EditorGUILayout.LabelField(gcValue);
                    EditorGUILayout.EndHorizontal();
                    EditorGUI.indentLevel--;
                    
                }
            }
            else
            {
                EditorGUILayout.LabelField("Field is null");
            }

            return true;
        }
    }

    public static class ExtensionMethods
    {
        public static Rect MoveDown(this Rect rect, int yoffset)
        {
            rect.y += yoffset;
            return rect;
        }
    }
}