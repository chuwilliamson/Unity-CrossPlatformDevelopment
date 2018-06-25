using System;
using System.Runtime.Serialization;

namespace RPGStats
{
    [DataContract]
    [Serializable]
    public class RPG_Stat
    {
        [DataMember] private readonly int _baseValue;

        [DataMember] public string Name;

        [IgnoreDataMember] public Action OnStatAdd;

        [DataMember] public int Value;

        public RPG_Stat()
        {
            Name = "Name:";
            Value = 0;
            _baseValue = Value;
            OnStatAdd += Doit;
        }

        public RPG_Stat(string n, int v)
        {
            Name = n;
            Value = v;
            _baseValue = Value;
            OnStatAdd += Doit;
        }

        private void Doit()
        {
        }

        public void Apply(Modifier mod)
        {
            if (mod.type == "add")
                Value += _baseValue + mod.value;
            if (mod.type == "mult")
                Value += _baseValue * mod.value / 10;
        }

        public void Remove(Modifier mod)
        {
            if (mod.type == "add")
                Value -= _baseValue + mod.value;
            if (mod.type == "mult")
                Value -= _baseValue * mod.value / 10;
        }
    }
}