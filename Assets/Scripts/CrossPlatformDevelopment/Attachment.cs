using System;
using UnityEngine;
using UnityEngine.Events;

public interface IAttachable
{
    void Attach(Transform target);
    void Detach();
    void Revert();
}

public class TransformCopy
{
    public Vector3 localPosition;
    public Quaternion localRotation;
    public Vector3 localScale;

    public TransformCopy(Transform t)
    {
        localPosition = t.localPosition;
        localRotation = t.localRotation;
        localScale = t.localScale;
    }
}

[Serializable]
public class Attachment : IAttachable
{
    private readonly TransformCopy _copy;
    private readonly GameObject _gameObject;
    private readonly Transform _origin;
    public UnityEvent OnWeaponAttached;

    public Attachment(GameObject original)
    {
        isAttached = false;
        OnWeaponAttached = new UnityEvent();
        _gameObject = original;
        _origin = _gameObject.transform.parent;
        _copy = new TransformCopy(_gameObject.transform);
    }

    public bool isAttached { get; private set; }


    public void Attach(Transform target)
    {
        _gameObject.transform.SetParent(target);
        _gameObject.transform.localPosition = Vector3.zero;
        _gameObject.transform.localRotation = Quaternion.identity;

        _gameObject.transform.FlipX();
        if (OnWeaponAttached != null)
            OnWeaponAttached.Invoke();
        isAttached = true;
    }

    public void Detach()
    {
        _gameObject.transform.SetParent(null);
    }

    public void Revert()
    {
        isAttached = false;
        _gameObject.transform.SetParent(_origin);
        _gameObject.transform.localPosition = _copy.localPosition;
        _gameObject.transform.localRotation = _copy.localRotation;
        _gameObject.transform.localScale = _copy.localScale;
    }
}