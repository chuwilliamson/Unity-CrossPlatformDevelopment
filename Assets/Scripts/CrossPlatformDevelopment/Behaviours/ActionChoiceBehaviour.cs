using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

public class ActionChoiceBehaviour : UIBehaviour
{
    public RadialLayout layout;

    public float step = 5f;

    public bool up;

    public void RadialLerp()
    {
        up = !up;
        StopAllCoroutines();
        StartCoroutine(Fan(up));
    }

    private IEnumerator Fan(bool up)
    {
        var condition = up ? layout.MaxAngle < 360 : layout.MaxAngle > 0;
        if (layout.MaxAngle > 360) layout.MaxAngle = 360;
        else if (layout.MaxAngle <= 0) layout.MaxAngle = 0;
        while (condition)
        {
            if (up) layout.MaxAngle += step;
            else if (!up) layout.MaxAngle -= step;

            layout.CalculateRadial();

            if (layout.MaxAngle > 360) layout.MaxAngle = 360;
            else if (layout.MaxAngle < 0) layout.MaxAngle = 0;
            yield return null;
        }

        yield return null;
    }
}