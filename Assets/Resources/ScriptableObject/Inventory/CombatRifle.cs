using UnityEngine;

[CreateAssetMenu(menuName = "Items/Gun")]
public class CombatRifle : Weapon, IShootable
{
    public GameObject projectilePrefab;
    public float RateOfFire;

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
    }

    public override void Execute()
    {
        Shoot(_owner);
    }

    public void Shoot(GameObject obj)
    {
        var go = Instantiate(projectilePrefab, obj.transform);
        go.transform.ResetTransformation();
        go.transform.localPosition += new Vector3(5, 0, 0);
        go.transform.SetParent(null);
        var thisrb = go.GetComponent<Rigidbody2D>();
        thisrb.AddForce(_owner.transform.forward * 5f, ForceMode2D.Impulse);
        Destroy(go, 2f);
    }

    
}