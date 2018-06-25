using UnityEngine;
namespace AIE
{
    //this class will get the input from the controller and store it to send to a movement behaviour
    public class PlayerInputBehaviour : MonoBehaviour
    {
        public MovementBehaviour MovementBehaviour;
        // Use this for initialization        
        public Vector3 TargetDirection;
        // Update is called once per frame
        void Update()
        {

            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            TargetDirection = h * Camera.main.transform.right + v * Camera.main.transform.forward;


            //before sending it to move we will transform it to camera space
            MovementBehaviour.Move(TargetDirection);
        }
    }
}