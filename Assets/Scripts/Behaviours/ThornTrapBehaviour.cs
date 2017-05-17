using UnityEngine;

public class ThornTrapBehaviour : MonoBehaviour
{
    private Modifier RUNTIME_MOD;
    public Modifier ThornModConfig;
    
    public void Initialize()
    {
        RUNTIME_MOD = Instantiate(ThornModConfig);
        RUNTIME_MOD.Initialize(null);
    }

    public void DoDamage(GameObject go)
    {
        go.GetComponent<PlayerBehaviour>().ModifyStat(RUNTIME_MOD.EffectedStat, RUNTIME_MOD.mod);
        Destroy(gameObject);
    }
}