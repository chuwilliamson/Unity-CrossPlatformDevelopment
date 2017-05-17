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

    void ModifyStat(StatModifier statMod)
    {
        if (_Stats.Items[statMod.EffectedStatType.ToString()] == null)
            return;

        var statName = statMod.EffectedStatType.ToString();

        switch (statMod.ModType)
        {
            case ModifierType.add:
                _Stats.Items[statName].Value = statMod.ModifiedValue + _Stats.Items[statMod.EffectedStatType.ToString()].Value;
                break;
            case ModifierType.mult:
                _Stats.Items[statName].Value = statMod.ModifiedValue * _Stats.Items[statMod.EffectedStatType.ToString()].Value;
                break;
        }
    }
}
