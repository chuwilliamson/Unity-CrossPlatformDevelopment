using Chuwilliamson.Variables;
using UnityEngine;

namespace Chuwilliamson.Testing
{
    public class Vector2VariableMonitor : MonoBehaviour
    {
        public Vector2Variable V2Variable;

        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            V2Variable.Value = new Vector2(Screen.width, Screen.height);
        }
    }
}