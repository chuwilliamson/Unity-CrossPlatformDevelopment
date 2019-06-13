using UnityEngine;
using System.Collections;


public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance
    {
        get { return SInstance; }
    }

    protected static PlayerInput SInstance;

    [HideInInspector]
    public bool playerControllerInputBlocked;

    protected Vector2 MMovement;
    protected Vector2 MCamera;
    protected bool MJump;
    protected bool MAttack;
    protected bool MPause;
    protected bool MExternalInputBlocked;

    public Vector2 MoveInput
    {
        get
        {
            if(playerControllerInputBlocked || MExternalInputBlocked)
                return Vector2.zero;
            return MMovement;
        }
    }

    public Vector2 CameraInput
    {
        get
        {
            if(playerControllerInputBlocked || MExternalInputBlocked)
                return Vector2.zero;
            return MCamera;
        }
    }

    public bool JumpInput
    {
        get { return MJump && !playerControllerInputBlocked && !MExternalInputBlocked; }
    }

    public bool Attack
    {
        get { return MAttack && !playerControllerInputBlocked && !MExternalInputBlocked; }
    }

    public bool Pause
    {
        get { return MPause; }
    }

    WaitForSeconds _mAttackInputWait;
    Coroutine _mAttackWaitCoroutine;

    const float KAttackInputDuration = 0.03f;

    void Awake()
    {
        _mAttackInputWait = new WaitForSeconds(KAttackInputDuration);

        if (SInstance == null)
            SInstance = this;
        else if (SInstance != this)
            throw new UnityException("There cannot be more than one PlayerInput script.  The instances are " + SInstance.name + " and " + name + ".");
    }


    void Update()
    {
        MMovement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        MCamera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MJump = Input.GetButton("Jump");

        if (Input.GetButtonDown("Fire1"))
        {
            if (_mAttackWaitCoroutine != null)
                StopCoroutine(_mAttackWaitCoroutine);

            _mAttackWaitCoroutine = StartCoroutine(AttackWait());
        }

        MPause = Input.GetButtonDown ("Pause");
    }

    IEnumerator AttackWait()
    {
        MAttack = true;

        yield return _mAttackInputWait;

        MAttack = false;
    }

    public bool HaveControl()
    {
        return !MExternalInputBlocked;
    }

    public void ReleaseControl()
    {
        MExternalInputBlocked = true;
    }

    public void GainControl()
    {
        MExternalInputBlocked = false;
    }
}
