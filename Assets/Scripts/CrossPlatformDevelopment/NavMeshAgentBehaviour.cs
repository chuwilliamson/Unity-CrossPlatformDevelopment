using UnityEngine;
using UnityEngine.AI;

namespace CrossPlatformDevelopment
{
    public class NavMeshAgentBehaviour : MonoBehaviour
    {
        private NavMeshAgent _agent;

        [SerializeField] private Transform target;

        // Use this for initialization
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        private void Update()
        {
            _agent.SetDestination(target.position);
        }
    }
}