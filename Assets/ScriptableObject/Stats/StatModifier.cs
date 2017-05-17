

using RPGStats;

using ScriptableAssets;

using UnityEngine;

public enum ModifierType
{
    add,
    mult
}

/// <summary>
/// The stat modifier class.
/// Scriptable Object base class for modifier scriptable objects.
/// </summary>
public abstract class StatModifier : ScriptableObject
{
    /// <summary>
    /// The effected stat type to modify.
    /// </summary>
    public Stat EffectedStat;

    /// <summary>
    /// The mod created to modify the stat.
    /// </summary>
    [HideInInspector]
    public RPGStats.Modifier mod;

    /// <summary>
    /// The mod type.
    /// </summary>
    public ModifierType ModType;

    /// <summary>
    /// The value to modify the stat with.
    /// </summary>
    public int ModifiedValue;

    /// <summary>
    /// The initialize function.
    /// </summary>
    /// <param name="go">
    /// The go.
    /// </param>
    public abstract void Initialize(GameObject go);
}
