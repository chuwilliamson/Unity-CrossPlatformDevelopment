using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Shields/Awesome Shield")]
public class AwesomeShield : Shield
{
    public Vector3 MaxScale;
    public Vector3 InitialScale;

    public bool isBlocking;

    public override void Block(GameObject blockedObject)
    {
        if (isBlocking)
            return;

        this._owner.transform.localScale = MaxScale;
        isBlocking = true;
    }

    public override void StopBlock()
    {
        if (!isBlocking)
            return;

        this._owner.transform.localScale = InitialScale;
        isBlocking = false;
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        if (ShieldGrowth == 0)
            ShieldGrowth = 1;
        ShieldGrowth = Mathf.Abs(ShieldGrowth);        
        if (!_owner)
            return;
        InitialScale = _owner.transform.localScale;
        MaxScale = _owner.transform.localScale * Mathf.Abs(ShieldGrowth);
    }
}
