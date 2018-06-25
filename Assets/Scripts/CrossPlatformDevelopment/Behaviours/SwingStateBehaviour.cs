using UnityEngine;

public class SwingStateBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("swinging", true);
        PlayerInput.UserControl = false;
        animator.GetComponent<PlayerAnimator>().SetWeapons(true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("swinging", false);
        animator.ResetTrigger("swing");
        PlayerInput.UserControl = true;
        animator.GetComponent<PlayerAnimator>().SetWeapons(false);
    }
}