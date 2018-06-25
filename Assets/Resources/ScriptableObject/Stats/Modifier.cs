using UnityEngine;

[CreateAssetMenu(menuName = "Modifier")]
public class Modifier : StatModifier {

    public override void Initialize(GameObject go)
    {
        mod = new RPGStats.Modifier(ModType.ToString(), EffectedStat.Name, ModifiedValue);
    }
}
