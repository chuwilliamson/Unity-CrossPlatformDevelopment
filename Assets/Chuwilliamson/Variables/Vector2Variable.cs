using UnityEngine;

namespace Chuwilliamson.Variables
{
    [CreateAssetMenu(menuName = "Variables/Vector2")]
    public class Vector2Variable : Variable
    {
        [SerializeField] private Vector2 value;

        public Vector2 Value
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