using Chuwilliamson.Variables;
using UnityEngine;

namespace Chuwilliamson.CrossPlatformDevelopment
{
    public class PlayerMovementBehaviour : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private Transform _myTransform;
        public Transform barrelTip;
        public Transform barrelTransform;
        public Transform baseTransform;

        [Header("Prefabs")] public GameObject bulletPrefab;

        [Header("Camera")] public Camera cameraRef;

        public float cooldownTime = 1;

        [Header("Gameplay")] public float gunpower = 15;

        [Header("Transforms")] public Transform helperBallTransform;
        public Vector3Variable mousePosition;
        public Vector3Variable mouseToWorld;
        public bool shotFired;

        [Header("Variables")] public FloatVariable speedVar;

        public float timer = 5;
        public Transform turretTransform;

        private void Start()
        {
            _myTransform = transform;
            if (cameraRef == null)
                cameraRef = Camera.main;
        }

        private void Update()
        {
            #region ------------------TheUpdate

            mousePosition.Value = Input.mousePosition;
            transform.position += PlayerInput.Move * speedVar.value;
            var camDistance = Vector3.Distance(cameraRef.transform.position, _myTransform.position);
            var mouseToWorldPoint = cameraRef.ScreenToWorldPoint(Input.mousePosition);
            mouseToWorldPoint += cameraRef.transform.forward * camDistance;
            mouseToWorld.Value = mouseToWorldPoint;
            turretTransform.LookAt(mouseToWorld.Value);
            helperBallTransform.position = mouseToWorld.Value;

            if (shotFired)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    timer = cooldownTime;
                    shotFired = false;
                }
            }

            #endregion
        }

        public void Shoot()
        {
            if (shotFired)
                return;
            shotFired = true;
            var go = Instantiate(bulletPrefab, barrelTip.position, Quaternion.identity);
            go.GetComponent<BulletBehaviour>().SetVelocity(turretTransform.forward * gunpower);
        }
    }
}