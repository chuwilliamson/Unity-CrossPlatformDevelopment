using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Chuwilliamson.ScriptableObjects
{
    public class GameDataWindow : EditorWindow
    {
        [SerializeField]
        private List<Sprite> sprites = new List<Sprite>();
        private FieldInfo _field;
        public string lookup;

        public List<Sprite> Sprites
        {
            get
            {
                if (sprites.Count <= 0)
                    sprites = AssetDatabase.FindAssets("t: Sprite", new[] { "Assets/Resources/RPG_inventory_icons" })
                        .Select(AssetDatabase.GUIDToAssetPath).Select(AssetDatabase.LoadAssetAtPath<Sprite>).ToList();

                return sprites;
            }
        }

        [MenuItem("Tools/GameData")]
        private static void Init()
        {
            var window = (GameDataWindow)GetWindow(typeof(GameDataWindow));
            window.Show();
        }

        public static void Save(string fileName, object data)
        {
            if (data == null)
                return;
            var jsonAsset = new TextAsset(JsonUtility.ToJson(data, true));
            AssetDatabase.CreateAsset(jsonAsset, "Assets/Resources/TextAssets/" + fileName.Split('.')[0] + ".asset");
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(jsonAsset));
            var filePath = Path.Combine(Application.streamingAssetsPath, fileName);
            File.WriteAllText(filePath, jsonAsset.text);
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
            lookup = EditorGUILayout.DelayedTextField("lookup ", lookup);
            if (GUILayout.Button("Save")) Save("GameSettings.json", this);

            if (GUILayout.Button("Load"))
            {
                var obj = Load<GameDataWindow>("GameSettings.json");
                lookup = obj.lookup;
            }

            if (GUILayout.Button("MakeThemAndSave"))
            {
                var header = "<b>The following file contains these assets</b> \n Type: Name\n";

                var names = string.Join(Environment.NewLine, Sprites.Select(n => n.GetType().Name + ":" + n.name + Environment.NewLine));


                var parent = new TextAsset(header + names);
                var assetName = "Items.asset";
                var path = Path.Combine("Assets/Chuwilliamson/Resources/Items/", assetName);
                AssetDatabase.CreateAsset(parent, path);

                foreach (var asset in Sprites)
                {
                    var obj = CreateInstance<Item>().Init(new Serialization.Item
                    { itemImage = asset, name = asset.name });
                    AssetDatabase.AddObjectToAsset(obj, parent);
                }
                var animation = new AnimationClip();

                AssetDatabase.AddObjectToAsset(animation, parent);
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(parent));
            }
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
}