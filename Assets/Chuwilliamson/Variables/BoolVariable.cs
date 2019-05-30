using UnityEngine;

namespace Chuwilliamson.Variables
{
    public class BoolVariable : Variable
    {
        [SerializeField] private bool value;

        public bool Value => value;

        public override string ToString()
        {
            return value.ToString();
        }
    }
}