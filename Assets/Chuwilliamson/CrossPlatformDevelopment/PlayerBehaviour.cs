sing UnityEngine;

namespace Chuwilliamson.CrossPlatformDevelopment
{
    public class PlayerBehaviour : MonoBehaviour
    {
        private int Health;

        private void Start()
        {
            GameSingleton.DamageTakenEvent.AddListener(TakeDamage);
        }

        private void TakeDamage(int amount)
        {
            Health -= amount;
        }
    }
}