
using UnityEngine;
using Variables;

[CreateAssetMenu]
public class Globals : ScriptableObject
{
    public void Print(string value)
    {
    Debug.Log(value);
    }
}