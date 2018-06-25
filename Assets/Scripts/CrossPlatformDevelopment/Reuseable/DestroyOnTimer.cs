using UnityEngine;
using UnityEngine.Events;

public class DestroyOnTimer : MonoBehaviour
{
    public OnDestroyed onDestroyed = new OnDestroyed();
    public float Time = 1.0f;

    private void OnEnable()
    {
        if (onDestroyed == null) onDestroyed = new OnDestroyed();
    }

    private void Update()
    {
        if (Time < 0)
            Destroy(gameObject);
        Time -= UnityEngine.Time.deltaTime;
    }

    private void OnDestroy()
    {
        if (onDestroyed != null)
            onDestroyed.Invoke(gameObject);
    }

    public class OnDestroyed : UnityEvent<GameObject>
    {
    }
}