using System.Collections.Generic;

namespace Combat
{
    public abstract class Entity
    {
        public string Name { get; set; }
        public Dictionary<string, CombatAction> Actions;
        public virtual void AddAction(string name, UnityEngine.Events.UnityAction action)
        {
            Actions.Add(name, new CombatAction { Name = name, Response = action });
        }
    }
}