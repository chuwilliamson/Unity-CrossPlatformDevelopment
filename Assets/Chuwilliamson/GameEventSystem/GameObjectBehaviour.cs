using UnityEngine;

namespace Chuwilliamson.GameEventSystem
{
    public class GameObjectBehaviour : MonoBehaviour
    {
        [SerializeField] private GameEvent onDestroyed;
        [SerializeField] private GameEvent onStart;
        [SerializeField] private GameEvent onUpdate;

        public bool raiseEventInUpdate;

        private void OnDestroy()
        {
            onDestroyed?.Raise(gameObject);
        }

        // Use this for initialization
        private void Start()
        {
            onStart?.Raise(gameObject);
        }

        private void Update()
        {
            if (!raiseEventInUpdate) return;
            onUpdate?.Raise(gameObject);
        }
    }
}