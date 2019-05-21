using UnityEngine;

namespace Chuwilliamson.GameEventSystem
{
    public class GameEventListener : MonoBehaviour, IListener
    {
        [SerializeField] private GameEvent gameEvent;
        [SerializeField] private GameEventResponse response;

        public void Subscribe()
        {
            gameEvent.RegisterListener(this);
        }

        public void UnSubscribe()
        {
            gameEvent.UnRegisterListener(this);
        }

        public void OnEventRaised(object[] args)
        {
            response.Invoke(args);
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            UnSubscribe();
        }
    }
}