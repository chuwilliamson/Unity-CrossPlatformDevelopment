using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif
public class Helpers
{
    private static HashSet<Object> objectsToUnload = new HashSet<Object>();

    private static void UnloadResources()
    {
        if (Application.isPlaying)
            return;

        foreach (var obj in objectsToUnload)
            Resources.UnloadAsset(obj);
        objectsToUnload.Clear();
    }

    public static T LoadResourceWithAutoUnload<T>(string name) where T : Object
    {
        var result = Resources.Load<T>(name);

#if UNITY_EDITOR
        if (result is ScriptableObject)
        {
            objectsToUnload.Add(result);
            EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;
        }
#endif

        return result;
    }

    private static void EditorApplication_playModeStateChanged(PlayModeStateChange obj)
    {
        UnloadResources();
    }
}