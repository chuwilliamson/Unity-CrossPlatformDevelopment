using Chuwilliamson.Variables;
using UnityEngine;

namespace CrossPlatformDevelopment
{
    public static class PlayerInput
    {
        public static Vector3 Move
        {
            get
            {
                var v3 = Resources.Load<Vector3Variable>("Variables/PlayerInput");
                v3.Value = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
                return v3.Value;
            }
        }
    }
}