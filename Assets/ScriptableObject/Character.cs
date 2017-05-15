using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "Characters/Default")]
public class Character : ScriptableObject
{
    public int ID;
    public string Name;
    public Stats _Stats;

    public virtual void OnEnable()
    {
        _Stats = Instantiate(_Stats);
    }
}
