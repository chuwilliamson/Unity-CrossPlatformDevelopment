using System.Collections.Generic;
using UnityEngine;

namespace Assessment
{
    [CreateAssetMenu]
    public class Squad : ScriptableObject
    {
        public List<PlayerData> squadData;
    }
}