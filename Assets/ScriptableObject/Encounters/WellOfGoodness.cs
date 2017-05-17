using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Encounter/Well of Good Good")]
public class WellOfGoodness : Encounter
{
    public override bool EncounterStart(GameObject obj)
    {
        obj.GetComponent<PlayerBehaviour>().ClearAll();
        Debug.Log("clear all modifiers");
        return true;
    }



}
