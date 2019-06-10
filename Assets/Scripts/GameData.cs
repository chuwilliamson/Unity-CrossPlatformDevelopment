using System.Collections.Generic;
using System.IO;
using System.Linq;
using Chuwilliamson.Serialization;
using Chuwilliamson.Variables;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    private static GameData _instance;
    private static string[] _paths;
    public StringListVariable keyListVariable;
    public ICollection<string> Keys => saveTable.Keys;

    [SerializeField]
    private Dictionary saveTable = new Dictionary();

    public ICollection<string> Values => saveTable.Values;

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

    public static Dictionary SaveData => Instance.saveTable;




    private void OnEnable()
    {
#if UNITY_EDITOR
        if (Instance != null)
        {
            if (Instance != this)
                AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(GetInstanceID().ToString()));
        }
        else
        {
            _instance = this;
        }
#endif

        _paths = new[]
        {
            Path.Combine(Application.streamingAssetsPath, "savetable.json"),
        };

        if (keyListVariable == null)
        {
#if UNITY_EDITOR

            AssetDatabase.CreateAsset(CreateInstance<StringListVariable>(),
                "Assets/Resources/KeyListVariable.asset");
            keyListVariable =
                AssetDatabase.LoadAssetAtPath<StringListVariable>("Assets/Resources/KeyListVariable.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif
        }

        if (keyListVariable != null)
        {
            keyListVariable.value = new List<string>(Instance.Keys);
        }

        if (File.Exists(_paths[0]))
        {
            savecopy = JsonUtility.FromJson<Dictionary>(File.ReadAllText(_paths[0]));
        }

    }
    [SerializeField]
    private Dictionary savecopy;
    private void OnDisable()
    {
        SaveToFile();
    }

    public void SaveToFile()
    {
        var jsons = new[]
        {
            JsonUtility.ToJson(saveTable, true),

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
            SaveData.Add(keyname, JsonUtility.ToJson(obj, prettyPrint:true));
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