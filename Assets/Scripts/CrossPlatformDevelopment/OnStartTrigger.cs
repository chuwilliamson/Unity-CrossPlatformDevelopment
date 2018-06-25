using System;
using UnityEngine;
using UnityEngine.Events;

public class OnStartTrigger : MonoBehaviour
{
    public OnStartEvent onStart;

    private void Start()
    {
        onStart.Invoke();
    }
}

[Serializable]
public class OnStartEvent : UnityEvent
{
}