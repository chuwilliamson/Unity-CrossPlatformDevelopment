using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

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

            if (GUILayout.Button("MakeThemAndSave"))
            {
                var assets = AssetDatabase.FindAssets("t: Sprite", new[] { "Assets/Resources/RPG_inventory_icons" })
                    .Select(AssetDatabase.GUIDToAssetPath).Select(AssetDatabase.LoadAssetAtPath<Sprite>).ToArray();

                foreach (var asset in assets)
                {
                    var obj = CreateInstance<Item>();
                    obj.Value = new Serialization.Item { itemImage = asset };
                    var path = Path.Combine("Assets/Chuwilliamson/Resources/Items/", asset.name + ".asset");
                    var loaded = AssetDatabase.LoadAssetAtPath<Item>(path);
                    AssetDatabase.CreateAsset(obj, path);

                }

            }
        }

        public List<Texture2D> images = new List<Texture2D>();
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