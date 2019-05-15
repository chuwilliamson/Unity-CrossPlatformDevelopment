using UnityEngine;

namespace Variables
{
    public class BoolVariable : Variable
    {
        [SerializeField] private bool value;

        public bool Value
        {
            get { return value; }
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}