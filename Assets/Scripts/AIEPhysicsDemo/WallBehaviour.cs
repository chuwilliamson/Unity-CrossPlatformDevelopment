using UnityEngine;

namespace AIEPhysicsDemo
{
    [RequireComponent(typeof(BoxCollider))]
    public class WallBehaviour : MonoBehaviour
    {
        public GameObject dude;

        public Transform spawnpoint;

        // Use this for initialization
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("make the dude because " + other.gameObject.name + " has entered my trigger");
                var d = Instantiate(dude, spawnpoint.position, Quaternion.identity);
                var rb = d.AddComponent<Rigidbody>();
                rb.AddForce(transform.forward * 25);
            }
        }
    }
}