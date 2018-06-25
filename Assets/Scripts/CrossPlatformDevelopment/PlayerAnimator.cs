using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private static readonly int KICKTRIGGER = Animator.StringToHash("kick");
    private static readonly int BLOCKING = Animator.StringToHash("block");
    private static readonly int FRONTSLIDE = Animator.StringToHash("slide");
    private static readonly int BACKSLIDE = Animator.StringToHash("backslide");
    private static readonly int SWINGTRIGGER = Animator.StringToHash("swing");
    private static readonly int PUNCHTRIGGER = Animator.StringToHash("punch");
    private static readonly int JUMPTRIGGER = Animator.StringToHash("jump");
    private static readonly int CROUCHING = Animator.StringToHash("crouch");

    
    
#pragma warning disable 0414
    private static readonly int ANIMSPEED = Animator.StringToHash("Speed");
#pragma warning restore 0414
    private IAttachable currentOffhand;

    private IAttachable currentWeapon;

    public Transform elbow_left;
    public Transform elbow_right;

    private Animator m_anim;
    public GameObject mainHand;
    public GameObject offHand;

    private Attachment shieldAttachment;
    public Transform spine;
    private Attachment swordAttachment;
    public Transform wrist_left;
    public Transform wrist_right;

    [ContextMenu("Set Body Parts")]
    public void SetBodyParts()
    {
        var hips = transform.Find("hips");
        spine = hips.transform.Find("spine");
        elbow_left = spine.transform.Find("chest/shoulder_left/elbow_left");
        elbow_right = spine.transform.Find("chest/shoulder_right/elbow_right");
        wrist_left = elbow_left.transform.Find("wrist_left");
        wrist_right = elbow_right.transform.Find("wrist_right");
    }

    private void Start()
    {
        shieldAttachment = new Attachment(offHand);
        swordAttachment = new Attachment(mainHand);
        currentWeapon = swordAttachment;
        currentOffhand = shieldAttachment;
        m_anim = GetComponent<Animator>();

        currentWeapon.Revert();
        currentOffhand.Revert();
        shieldAttachment.OnWeaponAttached.AddListener(() => GetComponent<AudioSource>().Play());
    }

    public void SetWeapons(bool on)
    {
        if (on)
        {
            currentWeapon.Attach(wrist_left);
            currentOffhand.Attach(elbow_right);
            offHand.transform.FlipX();
            return;
        }

        currentWeapon.Revert();
        currentOffhand.Revert();
    }

    private void Update()
    {
        if (PlayerInput.isInputBlock)
        {
            PlayerInput.UserControl = true;
            SetWeapons(false);
        }

        if (!PlayerInput.UserControl)
            return;

        if (Input.GetButtonDown(Strings.RIGHTBUMPER))
            m_anim.SetTrigger(KICKTRIGGER);

        if (Input.GetButtonDown(Strings.XBOX_Y))
            m_anim.SetTrigger(SWINGTRIGGER); //x key          

        if (Input.GetButtonDown(Strings.XBOX_X))
            m_anim.SetTrigger(PUNCHTRIGGER); //x key            

        if (Input.GetButton(Strings.LEFTBUMPER))
            m_anim.SetTrigger(BLOCKING);

        if (Input.GetButtonDown(Strings.JUMP))
            m_anim.SetTrigger(JUMPTRIGGER);


        m_anim.SetFloat(BACKSLIDE,
            Input.GetAxis(Strings.BSLIDE)); //r trigger axis 10                                    
        m_anim.SetFloat(FRONTSLIDE,
            Input.GetAxis(Strings.FSLIDE)); //r trigger axis 10                                    
        m_anim.SetBool(BLOCKING, PlayerInput.isInputBlock);
        m_anim.SetBool(CROUCHING, PlayerInput.isInputCrouch);
    }
}