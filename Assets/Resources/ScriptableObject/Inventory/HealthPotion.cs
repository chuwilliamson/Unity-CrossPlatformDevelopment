using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Potions/HealthPotions")]
public class HealthPotion : Potion
{
    public Modifier modifier;
    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        modifier = Instantiate(modifier);
        modifier.Initialize(obj);
    }

    public override void Execute()
    {        
        this.Consume(this._owner);
    }
    public override void Consume(GameObject owner)
    {
        owner.GetComponent<CharacterBehaviour>().ModifyStat(modifier.EffectedStat, modifier.mod);
    }
    
}
