using System.Collections;
using System.Collections.Generic;
using Chuwilliamson.CrossPlatformDevelopment;
using Gamekit3D;
using UnityEngine;

public class SetWeaponSMB : StateMachineBehaviour
{
    private MeleeWeapon Weapon;
    [SerializeField]
    private int index = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Weapon = PlayerController.instance.GetComponent<PlayerBehaviour>().Weapons[index];
        PlayerController.instance.meleeWeapon = Weapon;
    }
}
