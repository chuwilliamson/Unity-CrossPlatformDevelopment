using UnityEngine;

namespace Chuwilliamson
{
    [CreateAssetMenu]
    public class Globals : ScriptableObject
    {
        public void Print(string value)
        {
            Debug.Log(value);
        }
    }
}