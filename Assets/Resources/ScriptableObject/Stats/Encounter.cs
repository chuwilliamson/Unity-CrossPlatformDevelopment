using UnityEngine;

namespace ScriptableAssets
{
    public abstract class Encounter : ScriptableObject
    {
        private Object Result;
        public abstract void Begin();
        public abstract void End();
    }
}