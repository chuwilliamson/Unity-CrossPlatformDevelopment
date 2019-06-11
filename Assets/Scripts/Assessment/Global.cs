using System.Linq;
using UnityEditor;
using UnityEngine;
 
namespace Assessment
{
    [CreateAssetMenu(menuName = "UIDemo/Global")]
    public class Global : ScriptableObject
    {
        public UnityEngine.Events.UnityEvent Response;
        public static Global Instance
        {
            get
            {
                _sInstance = _sInstance ? _sInstance : Resources.Load<Global>("Global");
#if UNITY_EDITOR
                _sInstance = _sInstance ? _sInstance : AssetDatabase.FindAssets("t: Global").Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<Global>).First();
#endif
                
                return _sInstance;
            }
        }

        private static Global _sInstance;

        public void Print(string value)
        {
            Debug.Log(value);
        }

        public void Save(Object obj)
        {
            var path = Application.persistentDataPath + "/saves/" + obj.name + ".json";
            var json = JsonUtility.ToJson(obj, true);
            System.IO.File.WriteAllText(path, json);
        }
 
        public void Load(ref object obj)
        {
            var path = Application.persistentDataPath + "/saves/" + obj + ".json";
            var json = System.IO.File.ReadAllText(path);
            obj = JsonUtility.FromJson(json, obj.GetType());
        }

        public T Load<T>(Object obj) where T : ScriptableObject
        {
            var path = Application.persistentDataPath + "/saves/" + obj.name + ".json";
            var json = System.IO.File.ReadAllText(path);
            var clone = CreateInstance<T>();
            JsonUtility.FromJsonOverwrite(json, clone); 
            return clone;
        }
    }
     
}