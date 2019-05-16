using System.Collections.Generic;
using UnityEngine;

namespace GameEventSystem
{
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject, ISubscribeable
    {
        private List<IListener> _listeners = new List<IListener>();

        public void RegisterListener(IListener listener)
        {
            if (!_listeners.Contains(listener))
                _listeners.Add(listener);
        }

        public void UnRegisterListener(IListener listener)
        {
            if (_listeners.Contains(listener))
                _listeners.Remove(listener);
        }

        public void Raise(params object[] args)
        {
            for (var i = _listeners.Count - 1; i >= 0; i--) _listeners[i].OnEventRaised(args);
        }

        public void SelfRaise(Object obj)
        {
            Raise(obj);
        }

        public void Raise()
        {
            Raise(null);
        }
    }
}