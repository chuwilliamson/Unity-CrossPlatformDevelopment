using UnityEngine;

namespace Chuwilliamson.Variables
{
    [CreateAssetMenu(menuName = "Variables/int")]
    public class GameObjectVariable : Variable
    {
        [SerializeField] private GameObject value;


        public GameObject Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnValueChange?.Invoke(this);
            }
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}