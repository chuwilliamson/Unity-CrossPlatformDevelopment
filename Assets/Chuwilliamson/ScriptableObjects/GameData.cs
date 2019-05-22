using System.Collections.Generic;
using System.IO;
using System.Linq;
using Chuwilliamson.Variables;
using UnityEditor;
using UnityEngine;

namespace Chuwilliamson.ScriptableObjects
{
    [CreateAssetMenu]
    public class GameData : ScriptableObject, ISerializationCallbackReceiver
    {
        private static GameData _instance;
        private static string[] _paths;
        public StringListVariable keyListVariable;
        public List<string> keys = new List<string>();

        public Dictionary<string, string> saveTable;
        public List<string> values = new List<string>();

        public static GameData Instance
        {
            get
            {
                if (_instance) return _instance;
                _instance = Resources.Load<GameData>("GameData");
#if UNITY_EDITOR
                var vars = AssetDatabase.FindAssets("t:GameData")
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<GameData>)
                    .Where(b => b).OrderBy(v => v.name).ToArray();
                _instance = vars.FirstOrDefault();
                Debug.Log(_instance);
#endif

                return _instance;
            }
        }

        public static Dictionary<string, string> SaveData
        {
            get { return Instance.saveTable; }
        }

        public void OnBeforeSerialize() //before we hit play mode 
        {
            keys.Clear();
            values.Clear();

            foreach (var kvp in saveTable)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }


        public void OnAfterDeserialize()
        {
            saveTable = new Dictionary<string, string>();
            for (var i = 0; i < keys.Count; i++)
                saveTable.Add(keys[i], values[i]);
        }

        private void OnEnable()
        {
            _paths = new[]
            {
                Path.Combine(Application.streamingAssetsPath, "keys.json"),
                Path.Combine(Application.streamingAssetsPath, "values.json")
            };

            foreach (var kvp in saveTable)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }

            keyListVariable.value = Instance.keys;
        }

        private void OnDisable()
        {
            SaveToFile();
        }

        public static void SaveToFile()
        {
            var jsons = new[]
            {
                JsonUtility.ToJson(Instance.keys),
                JsonUtility.ToJson(Instance.values)
            };

            for (var i = 0; i < _paths.Length; i++)
                using (var sw = new StreamWriter(_paths[i]))
                {
                    sw.Write(jsons[i]);
                }
        }

        public static void Save<T>(T obj, string keyname)
        {
            if (obj == null)
                return;
            Debug.Log("saving " + obj);
            string json;
            if (SaveData.TryGetValue(keyname, out json))
                SaveData[keyname] = json;
            else
                SaveData.Add(keyname, JsonUtility.ToJson(obj));
        }

        public static T Load<T>(T obj, string keyname)
        {
            var json = "";
            var returnvalue = obj;
            if (!SaveData.TryGetValue(keyname, out json)) return returnvalue;
            returnvalue = JsonUtility.FromJson<T>(json);
            Debug.Log("loading " + obj);


            return returnvalue;
        }
    }
}