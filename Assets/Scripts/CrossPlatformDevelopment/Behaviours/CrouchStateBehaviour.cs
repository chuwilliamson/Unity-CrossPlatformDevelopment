using UnityEngine;

public class CrouchStateBehaviour : StateMachineBehaviour
{
    public float crouchspeed = 5f;
    public float movespeed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movespeed = animator.GetComponent<PlayerMovementBehaviour>().speed;
        animator.GetComponent<PlayerMovementBehaviour>().speed = 5f;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.GetComponent<PlayerMovementBehaviour>().speed = movespeed;
    }
}