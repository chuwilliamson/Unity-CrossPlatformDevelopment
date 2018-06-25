using UnityEngine;

public class CrouchStateBehaviour : StateMachineBehaviour
{
    public float crouchspeed = 5f;
    public float movespeed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movespeed = animator.GetComponent<RigidBodyPlayerMovementBehaviour>().speed;
        animator.GetComponent<RigidBodyPlayerMovementBehaviour>().speed = 5f;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.GetComponent<RigidBodyPlayerMovementBehaviour>().speed = movespeed;
    }
}