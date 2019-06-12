// ----------------------------------------------------------------------------
// AIE Morning Exercises - CrossPlatformDevelopment
// 
// Author: Matthew Williamson
// Date:   06/12/2019
// ----------------------------------------------------------------------------

using UnityEngine;

namespace MorningExercises
{
    /// <summary>
    ///     Morning Exercise: create a script that will teleport an object from one place to another place.
    ///     This script should be written with an artist in mind.
    ///     You win if @donaldg can use it without asking questions or becoming frustrated
    /// </summary>
    public class TeleportBehaviour : MonoBehaviour
    {
        public enum TeleportMode
        {
            RANDOM = 0,
            USERDEFINED = 1
        }

        [TextArea(0, 25)] public string _readmeText =
            "1. assign a target /n 2. assign a destination /n 3. make a button /n 4. drag this gameobject into the onclick in the button component " +
            "5.choose Teleport function from dropdown";
 
        [Header("Set this transform to teleport to")]
        public Transform destination;

        [Header("Set this transform for the object that will teleport")]
        public Transform source;

        [Header("Set the mode for teleportation")]
        public TeleportMode teleportMode = TeleportMode.USERDEFINED;

        /// <summary>
        ///     Teleport this GameObject to the destination field
        /// </summary>
        public void Teleport()
        {
            if (source == null)
            {
                Debug.LogWarning("You did not set a transform that will teleport... this will not work");
                return;
            }

            if (destination == null && teleportMode == TeleportMode.USERDEFINED)
            {
                Debug.LogWarning("You did not set a transform to teleport to... this will not work");
                return;
            }

            if (teleportMode == TeleportMode.RANDOM)
            {
                var go = new GameObject();
                var pos = Random.insideUnitCircle;
                go.transform.position = new Vector3(pos.x, 0, pos.y);
                SetTarget(go.transform);
            }


            source.position = destination.position;
        }

        /// <summary>
        ///     Set the target to teleport to
        /// </summary>
        /// <param name="target">the destination to teleport to</param>
        public void SetTarget(Transform target)
        {
            destination = target;
        }

        /// <summary>
        ///     Teleport to a destination
        /// </summary>
        /// <param name="target">the destination to teleport to</param>
        public void Teleport(Transform target)
        {
            SetTarget(target);
            Teleport();
        }
    }
}