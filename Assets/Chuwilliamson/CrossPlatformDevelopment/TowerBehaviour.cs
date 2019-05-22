using Chuwilliamson.Variables;
using UnityEngine;

namespace Chuwilliamson.CrossPlatformDevelopment
{
    public class TowerBehaviour : MonoBehaviour
    {
        private float _cooldownTime = 1.0f;

        private float _timer = 1.0f;
        public Transform barrelTip;
        public Transform barrelTransform;

        [Header("Prefabs")] public GameObject bulletPrefab;

        [SerializeField] private FloatReference gunPower;

        [SerializeField] private bool shotFired;

        [Header("Joints")] public Transform target;
        public Transform turretTransform;

        // Update is called once per frame
        private void Update()
        {
            if (target != null)
            {
                turretTransform.LookAt(target);
                Shoot();
            }


            if (shotFired)
            {
                _timer -= Time.deltaTime;
                if (_timer < 0)
                {
                    _timer = _cooldownTime;
                    shotFired = false;
                }
            }
        }

        /// <summary>
        ///     if you have a target dont' pick another one
        ///     we shoot it and wait for it to die
        /// </summary>
        /// <param name="target"></param>
        public void SetTarget(Transform target)
        {
            if (this.target != null)
                return;
            this.target = target;
        }


        public void Shoot()
        {
            if (shotFired)
                return;
            shotFired = true;
            if (bulletPrefab != null)
            {
                var go = Instantiate(bulletPrefab, barrelTip.position, Quaternion.identity);
                var rb2D = go.GetComponent<Rigidbody2D>();
                rb2D.AddForce(turretTransform.forward * gunPower.Value, ForceMode2D.Impulse);
            }
        }
    }
}