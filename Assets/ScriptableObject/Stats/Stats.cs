using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RPGStats;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Stats")]
public class Stats : ScriptableObject, IEnumerable<Stat>
{
    private readonly List<IDModifier> INSPECTOR_MODS = new List<IDModifier>();
    public Dictionary<string, Stat> Items = new Dictionary<string, Stat>();
    public Dictionary<int, RPGStats.Modifier> Modifiers = new Dictionary<int, RPGStats.Modifier>();
    public Stat[] StatsArray;

    public Stat this[string element]
    {
        get
        {
            if (Items.ContainsKey(element))
                return Items[element];
            return null;
        }

        set { Items[element] = value; }
    }

    public int Count
    {
        get { return StatsArray.Length; }
    }

    public IEnumerator<Stat> GetEnumerator()
    {
        return Items.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Items.Values.GetEnumerator();
    }

    private void OnEnable()
    {
        if (StatsArray == null) return;

        foreach (var stat in StatsArray)
            Add(Instantiate(stat));
    }

    public string AddModifier(int id, RPGStats.Modifier m)
    {
        INSPECTOR_MODS.Add(new IDModifier {identifier = id, mod = m});
        Modifiers.Add(id, m);
        var result = string.Format(
            "Add modifier {0} {1} {2}",
            Modifiers[id].stat,
            Modifiers[id].type,
            Modifiers[id].value);

        Items[m.stat].Apply(m);
        return result;
    }

    public string RemoveModifier(int id)
    {
        var result = string.Format("Remove modifier {0} {1} {2}", Modifiers[id].stat, Modifiers[id].type,
            Modifiers[id].value);
        Items[Modifiers[id].stat].Remove(Modifiers[id]);
        Modifiers.Remove(id);
        return result;
    }

    public void Add(Stat s)
    {
        s.Name = s.Name.Replace("(Clone)", string.Empty);
        Items.Add(s.Name, s);
    }

    public void ClearModifiers()
    {
        var keys = Modifiers.Keys.ToArray();
        foreach (var key in keys)
            RemoveModifier(key);

        Modifiers.Clear();
        INSPECTOR_MODS.Clear();
    }

    public Stat GetStat(string key)
    {
        return Items[key];
    }

    [Serializable]
    public class IDModifier
    {
        public int identifier;
        public RPGStats.Modifier mod;
    }
#if UNITY_EDITOR

    [CustomEditor(typeof(Stats))]
    public class InspectorLootTable : Editor
    {
        public override void OnInspectorGUI()
        {
            var mytarget = target as Stats;
            base.OnInspectorGUI();
            if (mytarget != null)
                foreach (var mod in mytarget.INSPECTOR_MODS)
                {
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("mod:" + mod.mod.stat, mod.mod.value.ToString());
                    GUILayout.EndHorizontal();
                }
        }
    }
#endif
}