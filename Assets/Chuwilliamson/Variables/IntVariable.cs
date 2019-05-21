using UnityEngine;

namespace Chuwilliamson.Variables
{
    [CreateAssetMenu(menuName = "Variables/int")]
    public class IntVariable : Variable
    {
        [SerializeField] private int value;

        public int Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}