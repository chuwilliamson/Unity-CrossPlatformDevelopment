using UnityEngine;

namespace Variables
{
    [CreateAssetMenu(menuName = "Variables/String")]
    public class StringVariable : Variable
    {
        [SerializeField] private string value;

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public override string ToString()
        {
            return value;
        }
    }
}