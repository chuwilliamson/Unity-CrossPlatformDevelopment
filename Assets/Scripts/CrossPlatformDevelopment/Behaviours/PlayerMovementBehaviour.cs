using System;
using UnityEngine;

[Serializable]
public class PlayerMovementBehaviour : MonoBehaviour
{
    internal static readonly int RHORIZONTAL = Animator.StringToHash("righthorizontal");
    internal static readonly int RVERTICAL = Animator.StringToHash("rightvertical");
    internal static readonly int LHORIZONTAL = Animator.StringToHash("lefthorizontal");
    internal static readonly int LVERTICAL = Animator.StringToHash("leftvertical");
    
    private Rigidbody2D _rigidbody2D;
    private bool _rootMotion;

    public float crouchspeed = 5f;
    public GameObject IKSpine;

    public float jumpPower = 600f;
    private Vector3 lookat;
    private Animator m_anim;

    private float scaleF;


    [Range(0, 100)]
    public float speed = 15f;



    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        scaleF = transform.localScale.x;
        _direction = Vector3.right;
    }

    private Vector3 _direction;
    public Vector3 Direction
    {
        get { return _direction; }
    }
    private void Update()
    {
        if(_rootMotion)
            return;

        lookat = new Vector3(PlayerInput.RightStick.x, PlayerInput.RightStick.y, 0);
        Debug.DrawLine(transform.position, transform.position + lookat * 15f, Color.blue);
        var move = new Vector3(PlayerInput.LeftStick.x, PlayerInput.LeftStick.y, 0);
        var dir = move.normalized;

        m_anim.SetFloat(RHORIZONTAL, PlayerInput.RightStick.x);
        m_anim.SetFloat(RVERTICAL, PlayerInput.RightStick.y);
        m_anim.SetFloat(LHORIZONTAL, PlayerInput.LeftStick.x);
        m_anim.SetFloat(LVERTICAL, PlayerInput.LeftStick.y);

        if(PlayerInput.UserControl)
        {
            if(_rigidbody2D.velocity.magnitude > 0)
            {
                var dot = Vector3.Dot(dir, transform.right);

                if (dot < 0 || PlayerInput.RightStick.x < 0)
                {
                    SetScaleX(transform, -scaleF);
                    _direction = Vector3.left;
                }
                    
                else if (dot > 0 || PlayerInput.RightStick.x > 0)
                {
                    SetScaleX(transform, scaleF);
                    _direction = Vector3.right;
                }
                    
            }


            _rigidbody2D.velocity = new Vector3(move.x * speed, _rigidbody2D.velocity.y, 0);
        }
    }

    private void OnAnimatorMove()
    {
        transform.position = m_anim.rootPosition;
        _rootMotion = m_anim.deltaPosition.magnitude > 0;
    }


    public static void SetScaleX(Transform t, float val)
    {
        var scale = t.localScale;
        scale.x = val;
        t.localScale = scale;
    }

    public static void SetScaleY(Transform t, float val)
    {
        var scale = t.localScale;
        scale.x = val;
        t.localScale = scale;
    }
}