using System;
using UnityEngine.Events;

namespace Chuwilliamson.GameEventSystem
{
    [Serializable]
    public class GameEventResponse : UnityEvent<object[]>
    {
    }
}