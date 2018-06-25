using System;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR

#endif

public class OnPhysicsTrigger : MonoBehaviour
{
    [TagSelector] public string ListenerTag;
    public OnStart onStart = new OnStart();
    public OnEnterCollision onEnterCollision = new OnEnterCollision();
    public OnEnterTrigger onEnterTrigger = new OnEnterTrigger();
    public OnExitCollision onExitCollision = new OnExitCollision();
    public OnExitTrigger onExitTrigger = new OnExitTrigger();
    
    /// <summary>
    /// invoke start callbacks
    /// </summary>
    private void Start()
    {
        onStart.Invoke();
    }

    /// <summary>
    /// if the listener tag matches what we enter then invoke trigger enter callbacks, 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ListenerTag))
            onEnterTrigger.Invoke(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ListenerTag))
            onExitTrigger.Invoke(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ListenerTag))
            onEnterCollision.Invoke(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ListenerTag))
            onExitCollision.Invoke(collision.gameObject);
    }


    [Serializable]
    public class OnStart : UnityEvent { }

    [Serializable]
    public class OnExitTrigger : UnityEvent<GameObject> { }

    [Serializable]
    public class OnEnterTrigger : UnityEvent<GameObject> { }

    [Serializable]
    public class OnEnterCollision : UnityEvent<GameObject> { }

    [Serializable]
    public class OnExitCollision : UnityEvent<GameObject> { }
}