using UnityEngine;

//It is common to create a class to contain all of your
//extension methods. This class must be static.
public static class ExtensionMethods
{
    //Even though they are used like normal methods, extension
    //methods must be declared static. Notice that the first
    //parameter has the 'this' keyword followed by a Transform
    //variable. This variable denotes which class the extension
    //method becomes a part of.
    public static void ResetTransformation(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }

    public static void Stretch(this RectTransform source)
    {
        source.anchorMin = new Vector2(0, 0);
        source.anchorMax = new Vector2(1, 1);
        source.pivot = new Vector2(.5f, .5f);
        source.offsetMax = Vector2.zero;
        source.offsetMin = Vector2.zero;
        source.localScale = new Vector3(1, 1, 1);
    }

    public static void FlipX(this Transform t)
    {
        var scale = t.localScale;
        scale.x = t.localScale.x * -1f;
        t.localScale = scale;
    }

    public static void SetYPos(this GameObject source, float yval)
    {
        var position = new Vector3(source.transform.position.x, yval, source.transform.position.z);
        source.transform.position = position;
    }

    public static void ToggleActiveSelf(this GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }
}