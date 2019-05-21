using UnityEngine;

namespace Chuwilliamson.Variables
{
    [CreateAssetMenu(menuName = "Variables/Vector3")]
    public class Vector3Variable : Variable
    {
        [SerializeField] private Vector3 value;

        public Vector3 Value
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