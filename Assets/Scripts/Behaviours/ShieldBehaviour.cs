using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{        
    public Vector2 MyColliderOffset;
    public Vector2 MyColliderSize;

    public Shield _ShieldConfig;

    public IBlockable CurrentShield;

    public void Initialize()
    {
        _ShieldConfig = Instantiate(_ShieldConfig);       
        _ShieldConfig.Initialize(this.gameObject);
        CurrentShield = _ShieldConfig;

        MyColliderOffset = GetComponent<BoxCollider2D>().offset;
        MyColliderSize = GetComponent<BoxCollider2D>().size;    
    }    

    public void DoBlock(GameObject go)
    {        
        if (go == this.transform.parent)
            return;
        CurrentShield.Block(go);
        if(_ShieldConfig.ShieldGrowth < 1)
        {
            GetComponent<BoxCollider2D>().offset = MyColliderOffset * 2;
            GetComponent<BoxCollider2D>().size = MyColliderSize * 2;
        }                        
    }
    
    public void StopBlock(GameObject go)
    {
        if (go == this.transform.parent)
            return;
        CurrentShield.StopBlock();
        GetComponent<BoxCollider2D>().offset = MyColliderOffset;
        GetComponent<BoxCollider2D>().size = MyColliderSize;
    }
}
