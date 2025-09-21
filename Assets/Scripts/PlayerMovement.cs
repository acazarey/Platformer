using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    [Header("Attack")]
    [SerializeField] private PlayerAttackHitbox attackCtrl; // drag the child AttackHitbox here

    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private Vector3 baseScale;

    // Animator params
    private static readonly int RunHash       = Animator.StringToHash("run");
    private static readonly int JumpTrigHash  = Animator.StringToHash("jump");
    private static readonly int GroundedHash  = Animator.StringToHash("grounded");
    private static readonly int AttackTrigHash= Animator.StringToHash("attack");

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        baseScale = transform.localScale; // e.g., (1.3, 1.3, 1.3)
    }

    private void Update()
    {
        // --- horizontal move ---
        float x = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(x * moveSpeed, body.velocity.y);

        // --- flip (preserve scale magnitude; your art faces left by default) ---
        if (Mathf.Abs(x) > 0.01f)
        {
            float mag = Mathf.Abs(baseScale.x);
            float sign = (x > 0f) ? -1f : 1f; // right -> negative X, left -> positive X
            transform.localScale = new Vector3(mag * sign, baseScale.y, baseScale.z);
        }

        // --- jump from any state when grounded ---
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            grounded = false;
            anim.SetBool(GroundedHash, false);
            anim.ResetTrigger(JumpTrigHash);
            anim.SetTrigger(JumpTrigHash);
        }

        // --- attack from any state (J or left mouse) ---
        if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0))
        {
            anim.ResetTrigger(AttackTrigHash);
            anim.SetTrigger(AttackTrigHash);
            if (attackCtrl) StartCoroutine(attackCtrl.AttackWindow()); // enables the hitbox briefly
        }

        // --- drive run (don’t show run while attacking) ---
        bool inAttack = anim.GetCurrentAnimatorStateInfo(0).IsName("attack");
        anim.SetBool(RunHash, Mathf.Abs(x) > 0.01f && grounded && !inAttack);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            anim.SetBool(GroundedHash, true);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            anim.SetBool(GroundedHash, false);

            // falling off platforms should also play jump
            anim.ResetTrigger(JumpTrigHash);
            anim.SetTrigger(JumpTrigHash);
        }
    }
}