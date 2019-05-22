using UnityEngine;
using UnityEngine.Events;

namespace Chuwilliamson.CrossPlatformDevelopment
{
    public class CrossWallBehaviour : MonoBehaviour
    {
        [SerializeField] private string compareTag = "Player";

        public int Health = 10;

        [SerializeField] private UnityEvent Response;

        private void Start()
        {
            GameSingleton.DamageTakenEvent.AddListener(TakeDamage);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(compareTag)) Response.Invoke();
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
        }
    }
}