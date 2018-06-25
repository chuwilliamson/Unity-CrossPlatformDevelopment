using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AIE
{


    public class SetLookAtBehaviour : MonoBehaviour
    {
        public Transform Target;
        public Cinemachine.CinemachineVirtualCamera vcam;
        [ContextMenu("follow")]
        public void FollowAtTarget()
        {
            vcam.Follow = Target;
        }
        [ContextMenu("lookat")]
        public void LookAtTarget()
        {
            vcam.LookAt = Target;
        }
    }
}