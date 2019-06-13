using UnityEngine;
namespace CrossPlatformDevelopment
{
    public class PlayerMovementBehaviour : MonoBehaviour
    {
        CharacterController characterController;
        Animator animator;
        private float speed = 5.0f;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");
            characterController.SimpleMove(new Vector3(h, 0, v) * speed);
            animator.SetFloat("speed", characterController.velocity.magnitude);
            
        }


    }
}