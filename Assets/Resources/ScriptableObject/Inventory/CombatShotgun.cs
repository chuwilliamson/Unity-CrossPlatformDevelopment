using UnityEngine;

[CreateAssetMenu(menuName = "Items/ShotGun")]
public class CombatShotgun : Weapon, IShootable
{
    readonly int numPellets = 5;

    public GameObject projectilePrefab;

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
        for(var i = 0; i < numPellets; i++)
        {
            var pb = _owner.GetComponent<PlayerMovementBehaviour>();
            var go = Instantiate(projectilePrefab, _owner.transform.position, _owner.transform.rotation);
            go.transform.localPosition += new Vector3(Mathf.Cos(i), Mathf.Sin(i), 0) + pb.Direction * 5f;
            var thisrb = go.GetComponent<Rigidbody2D>();
            thisrb.AddForce(pb.Direction * 25f, ForceMode2D.Impulse);
            Destroy(go, 2f);
        }
    }


}