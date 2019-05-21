using UnityEngine;

namespace AIEPhysicsDemo
{
    /// <summary>
    ///     Change the color of this gameObject to a random color when it "hits" the "Ground"
    /// </summary>
    public class BallHitGroundBehaviour : MonoBehaviour
    {
        [SerializeField] private MeshRenderer theMeshRenderer;

        private void Start()
        {
            if (theMeshRenderer == null)
                theMeshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ground")) theMeshRenderer.material.color = Random.ColorHSV();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Wall"))
                other.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        }
    }
}