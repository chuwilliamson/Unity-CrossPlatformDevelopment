using UnityEngine;

namespace ScriptableAssets
{
    [CreateAssetMenu]
    public class Unit : ScriptableObject
    {
        [HideInInspector] public Stats _stats;

        public string Name;


    }
}