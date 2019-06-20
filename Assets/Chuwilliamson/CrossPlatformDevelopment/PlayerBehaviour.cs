using UnityEngine;
using Gamekit3D;
namespace Chuwilliamson.CrossPlatformDevelopment
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField]
        private MeleeWeapon[] weapons;

        public MeleeWeapon[] Weapons => weapons;
    }
}