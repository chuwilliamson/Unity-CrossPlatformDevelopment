using UnityEngine;
using Variables;

public static class PlayerInput
{
    public static Vector3 Move
    {
        get
        {
            var v3 = Resources.Load<Vector3Variable>("PlayerInput");
            v3.Value = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            return v3.Value;
        }
    }
}