using System.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Serialization
{
    [CreateAssetMenu]
    public class GameData : ScriptableObject, ISerializationCallbackReceiver
    {
        public void Save(Object obj)
        {
            Save<Object>(obj);
        }

        private static Dictionary<string, string> _saveTable = new Dictionary<string, string>();
        public List<string> keys;
        public List<string> values;
        public Dictionary<string, string> _serializedDictionary = new Dictionary<string, string>();
        public static Dictionary<string, string> SaveData
        {
            get { return _saveTable; }
        }

        public static void Save<T>(T obj)
        {
            var path = Path.Combine(Application.streamingAssetsPath, obj + ".json");
            var json = JsonUtility.ToJson(obj);
            if (_saveTable.ContainsKey(obj.ToString()))
            {
                _saveTable[obj.ToString()] = json;
            }
            else
            {
                _saveTable.Add(obj.ToString(), json);
            }

            
            
        }

        public static T Load<T>(T obj)
        {
            //var path = Path.Combine(Application.streamingAssetsPath, obj  + ".json");
            //var json = File.ReadAllText(path);

            string json;

            if (_saveTable.TryGetValue(obj.ToString(), out json))
            {
                return JsonUtility.FromJson<T>(json);
            }

            return default(T);
        }

        public void OnBeforeSerialize()
        {
            keys = new List<string>();
            values = new List<string>();
            foreach (var kvp in _saveTable)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }


        }

        public void OnAfterDeserialize()
        {
            _serializedDictionary = new Dictionary<string, string>();
            for (int i = 0; i < keys.Count; i++)
                _serializedDictionary.Add(keys[i], values[i]);
        }
    }
}