using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spikey Shield", menuName = "Items/Shields/Spikey Shield")]
public class SpikeyShield : ShieldConfig
{
#pragma warning disable 0649
    Anima2D.SpriteMesh spikeyShieldMesh;
#pragma warning restore 0649
    public override void Block(GameObject blockedObject)
    {
        if(blockedObject.GetComponentInParent<CharacterBehaviour>())
            blockedObject.GetComponentInParent<CharacterBehaviour>().ModifyStat(_StatModifier.EffectedStat, _StatModifier.mod);
    }

    public override void StopBlock()
    {

    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        _StatModifier = Instantiate(_StatModifier);
        _StatModifier.Initialize(null);
        obj.GetComponent<Anima2D.SpriteMeshInstance>().spriteMesh = spikeyShieldMesh;
    }

    public Modifier _StatModifier;
}
