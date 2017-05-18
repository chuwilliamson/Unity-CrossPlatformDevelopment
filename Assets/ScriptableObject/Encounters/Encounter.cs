using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Encounter : ScriptableObject {

    /// <summary>
    /// Boolean variable that helps guide the EncounterBehavior
    /// Is true if it is limited by an area
    /// Is false if it is considered persistant until the End conditions are met
    /// </summary>
    public virtual bool Contained //Variable name up for change
    {
        get
        {
            return true;
        }
    }

    public virtual bool EncounterStart(GameObject obj)
    {
        return EncounterStart();
    }
    public virtual bool EncounterStart()
    {
        return true;
    }

    public virtual bool EncounterUnderway()
    {
        return true;
    }

    public virtual bool EncounterEnd()
    {
        return true;
    }

    public virtual void Initialize(GameObject obj)
    {
    }
}
