using System.Linq;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

#endif


namespace Chuwilliamson.Attributes
{
    /// <summary>
    ///     add this attribute to display a dropdown list of all scriptableobject types
    ///     this will disable the ability to select an object reference through the object field property drawer
    /// </summary>
    public class ScriptVariableAttribute : PropertyAttribute
    {
        public readonly ScriptableObject[] Vars;
        //ToDo:: Infer type from the field this attribute is applied to.

        public ScriptVariableAttribute()
        {
#if UNITY_EDITOR
            Vars = AssetDatabase.FindAssets("t:ScriptableObject").Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<ScriptableObject>).Where(b => b).OrderBy(v => v.name).ToArray();
#endif
        }

        public bool Verbose { get; set; }
    }
}