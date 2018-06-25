using UnityEngine;
namespace AIE
{
    public class PlayerMovementBehaviour : MovementBehaviour
    {
        private CharacterController controller;
        public float Speed = 5.0f;

        private void Start()
        {
            //assign the charactercontroller reference
            controller = GetComponent<CharacterController>();
        }

        /// <summary>
        /// move the player by moving the Character Controller
        /// </summary>
        /// <param name="movement"></param>
        public override void Move(Vector3 movement)
        {
            controller.SimpleMove(movement * Speed);
        }
    }
}