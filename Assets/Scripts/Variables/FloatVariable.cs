using UnityEngine;

namespace Variables
{
    [CreateAssetMenu(menuName = "Variables/float")]
    public class FloatVariable : Variable
    {
        [SerializeField] private float value;

        public float Value
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