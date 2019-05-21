using UnityEngine;

namespace CrossPlatformDevelopment
{
    public class BulletBehaviour : MonoBehaviour
    {
        [SerializeField] private float ttl = 1;

        public Vector3 Velocity { get; set; }

        public void SetVelocity(Vector3 v)
        {
            Velocity = v;
        }

        private void Update()
        {
            if (ttl < 0)
                Destroy(gameObject);
        }
    }
}