using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.Assertions;

public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject
{
    protected static T _instance;

    public static T Instance
    {
        get
        {
            if (!_instance)
                _instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
            if(!_instance)
                _instance = CreateInstance<T>();
            return _instance;
        }
    }
}

/// <summary>
/// class to track data and save to a location
/// </summary>
[CreateAssetMenu]
public class DataController : ScriptableSingleton<DataController>
{
    private static string path;
    private static int numsaves = 0;
    public static void Save<T>(T data, string profilename = "") where T : ScriptableObject
    {        
        path = Application.dataPath + "/StreamingAssets/";
        var files = System.IO.Directory.GetFiles(path, "*.json").ToList();
        numsaves = files.Count;
        //object to json string
        var json = JsonUtility.ToJson(data, true);
        //filename to save
        var savename = data.GetType().ToString();
        if (profilename != "")
            savename = profilename;
        
        var filename = string.Format("{0}-{1}.json", savename, numsaves);
        //write all text to the file at the path
        File.WriteAllText(path + filename, json);
        numsaves++;
        Debug.LogFormat("save {0} with filename {1}", data, filename);
    }

    public static T Load<T>(string filename) where T : ScriptableObject
    {
        path = Application.dataPath + "/StreamingAssets/";
        var json = File.ReadAllText(path + filename+".json");
        var scriptableObject = ScriptableObject.CreateInstance<T>();
        JsonUtility.FromJsonOverwrite(json, scriptableObject);
        if (scriptableObject == null)
            Debug.Log("could not load requested file");
        return scriptableObject;
    }

}