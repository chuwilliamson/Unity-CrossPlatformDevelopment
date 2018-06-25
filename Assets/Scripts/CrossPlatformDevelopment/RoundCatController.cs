using UnityEngine;

public class RoundCatController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private float dist;
    public float FollowDistance;
    public bool FollowPlayer = true;
    public Transform FollowTarget;

    public bool walk;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        if (FollowPlayer)
            FollowTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (FollowTarget.position.x < transform.position.x)
            transform.localScale = new Vector3(-.5f, .5f, 0);
        else
            transform.localScale = new Vector3(.5f, .5f, 0);
        dist = Vector3.Distance(transform.position, FollowTarget.position);
        if (dist > FollowDistance * 1.5f)
            _animator.SetFloat("speed", 1.5f);
        else
            _animator.SetFloat("speed", 1f);
        _animator.SetBool("walk", dist > FollowDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _animator.SetBool("walk", false);
    }
}