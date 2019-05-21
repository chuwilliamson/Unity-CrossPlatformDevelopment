using System.Collections.Generic;
using System.IO;
using System.Linq;
using Chuwilliamson.Variables;
using UnityEngine;

namespace Chuwilliamson.ScriptableObjects
{
    [CreateAssetMenu]
    public class GameData : ScriptableObject, ISerializationCallbackReceiver
    {
        private static GameData _instance;
        private static string[] _paths;

        private Dictionary<string, string> _saveTable = new Dictionary<string, string>();
        public StringListVariable keyListVariable;
        public List<string> keys = new List<string>();
        public List<string> values = new List<string>();

        public static GameData Instance
        {
            get
            {
                if (!_instance)
                    _instance = Resources.FindObjectsOfTypeAll<GameData>().FirstOrDefault();

                return _instance;
            }
        }

        public static Dictionary<string, string> SaveData
        {
            get { return Instance._saveTable; }
        }

        public void OnBeforeSerialize() //before we hit play mode 
        {
            keys.Clear();
            values.Clear();

            foreach (var kvp in _saveTable)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }


        public void OnAfterDeserialize()
        {
            _saveTable = new Dictionary<string, string>();
            for (var i = 0; i < keys.Count; i++)
                _saveTable.Add(keys[i], values[i]);
        }

        private void OnEnable()
        {
            _paths = new[]
            {
                Path.Combine(Application.streamingAssetsPath, "keys.json"),
                Path.Combine(Application.streamingAssetsPath, "values.json")
            };
            foreach (var kvp in _saveTable)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }

            keyListVariable.Value = keys;
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
            if (SaveData.TryGetValue(keyname, out json))
            {
                returnvalue = JsonUtility.FromJson<T>(json);
                Debug.Log("loading " + obj);
            }


            return returnvalue;
        }
    }
}