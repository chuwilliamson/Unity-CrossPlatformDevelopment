using System.Collections.Generic;
using UnityEngine;

namespace CrossPlatformDevelopment
{
    public class ShootableBehaviour : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
            var tb = FindObjectOfType<TowerBehaviour>();
            tb.SetTarget(transform);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                var pieces = new List<GameObject>();
                for (var i = 0; i < 5; i++)
                {
                    var piece = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    piece.transform.position = transform.position;
                    piece.AddComponent<Rigidbody>();
                    pieces.Add(piece);
                }

                foreach (var p in pieces)
                {
                    p.GetComponent<Rigidbody>().AddExplosionForce(25, p.transform.position, 5);
                    Destroy(p, 2);
                }

                Destroy(gameObject, 1);
            }
        }
    }
}