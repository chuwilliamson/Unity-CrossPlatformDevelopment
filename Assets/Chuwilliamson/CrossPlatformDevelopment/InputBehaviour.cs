using UnityEngine;

namespace Chuwilliamson.CrossPlatformDevelopment
{
    public class InputBehaviour : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) GameSingleton.DamageTakenEvent.Invoke(5);
        }
    }
}