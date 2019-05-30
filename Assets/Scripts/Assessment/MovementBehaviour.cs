using UnityEngine;

namespace Assessment
{
    /// <summary>
    /// Base class for implementing movement
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    [DisallowMultipleComponent]
    public class MovementBehaviour : MonoBehaviour
    {
        private CharacterController _characterController;

        [SerializeField] protected Vector3 moveVector = Vector3.forward;

        [SerializeField] protected float speed = 1.0f;

        /// <summary>
        /// initialize the character controller
        /// initialize moveVector
        /// </summary>
        protected virtual void Start()
        {
            _characterController = GetComponent<CharacterController>();
            moveVector = transform.forward * speed;
        }

        /// <summary>
        /// Call this in update from derived class via base.Move()
        /// </summary>
        protected virtual void Move()
        {
            _characterController.SimpleMove(moveVector);
        }
    }
}