using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace RPGStats
{
    [DataContract]
    [Serializable]
    public class RPG_Stats : IEnumerable<RPG_Stat>
    {
        [DataMember] public Dictionary<string, RPG_Stat> _items;

        [DataMember] public Dictionary<int, Modifier> modifiers;

        public RPG_Stats(params RPG_Stat[] s)
        {
            _items = new Dictionary<string, RPG_Stat>();
            modifiers = new Dictionary<int, Modifier>();
            foreach (var stat in s)
                _items.Add(stat.Name, stat);
        }

        public RPG_Stat this[string element]
        {
            get
            {
                if (_items.ContainsKey(element))
                    return _items[element];
                return null;
            }

            set { _items[element] = value; }
        }

        public IEnumerator<RPG_Stat> GetEnumerator()
        {
            return _items.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.Values.GetEnumerator();
        }

        public virtual string AddModifier(int id, Modifier m)
        {
            modifiers.Add(id, m);
            var result = string.Format(
                "Add modifier {0} {1} {2}",
                modifiers[id].stat,
                modifiers[id].type,
                modifiers[id].value);


            _items[m.stat].Apply(m);
            return result;
        }

        public virtual string RemoveModifier(int id)
        {
            var result = string.Format("Remove modifier {0} {1} {2}", modifiers[id].stat, modifiers[id].type,
                modifiers[id].value);
            _items[modifiers[id].stat].Remove(modifiers[id]);
            modifiers.Remove(id);
            return result;
        }

        public virtual void Add(RPG_Stat s)
        {
            _items.Add(s.Name, s);
        }

        public virtual void ClearModifiers()
        {
            var keys = modifiers.Keys.ToArray();
            foreach (var key in keys)
                RemoveModifier(key);
            modifiers.Clear();
        }

        public virtual RPG_Stat GetStat(string name)
        {
            return _items[name];
        }
    }
}