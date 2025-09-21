using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SidetoSide : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementDistance = 2f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool movingLeft = true;

    [Header("Death")]
    [SerializeField] private string attackTag = "PlayerAttack"; // tag on player's hitbox
    [SerializeField] private float despawnDelay = 0.35f;        // match death clip length
    [SerializeField] private string deathStateName = "die";     // Animator state name (optional)

    private float leftEdge, rightEdge;
    private bool dead;

    private SpriteRenderer sr;
    private Vector3 baseScale;
    private Animator anim;
    private Collider2D col;

    private static readonly int DieTrigHash = Animator.StringToHash("die");

    private void Awake()
    {
        baseScale = transform.localScale;
        sr   = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col  = GetComponent<Collider2D>();

        // NOTE: this enemy collider should be Non-Trigger. Uncheck "Used By Effector".
        if (col) { col.isTrigger = false; }

        float x0 = transform.position.x;
        leftEdge  = x0 - movementDistance;
        rightEdge = x0 + movementDistance;

        ApplyFlip();
    }

    private void Update()
    {
        if (dead) return;

        float x = transform.position.x;
        float step = speed * Time.deltaTime;

        if (movingLeft)
        {
            if (x > leftEdge)  transform.position = new Vector3(x - step, transform.position.y, transform.position.z);
            else { movingLeft = false; ApplyFlip(); }
        }
        else
        {
            if (x < rightEdge) transform.position = new Vector3(x + step, transform.position.y, transform.position.z);
            else { movingLeft = true; ApplyFlip(); }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player's attack hitbox must be a Trigger with tag "PlayerAttack"
        if (dead) return;
        Debug.Log($"Enemy contact with: {other.name} (tag: {other.tag})");
        if (other.CompareTag(attackTag))
        Die();
    }

    public void Die()
    {
        if (dead) return;
        dead = true;

        speed = 0f;                  // stop moving
        if (col) col.enabled = false;

        // play death animation if available
        if (anim)
        {
            anim.ResetTrigger(DieTrigHash);
            anim.SetTrigger(DieTrigHash);
            // Option A: timed destroy
            Destroy(gameObject, despawnDelay);
            // Option B (better): add an Animation Event that calls Despawn() at clip end.
        }
        else
        {
            // no animator? just hide and destroy
            if (sr) sr.enabled = false;
            Destroy(gameObject, 0.05f);
        }
    }

    // Called by an Animation Event at the end of the death clip (optional)
    public void Despawn() => Destroy(gameObject);

    private void ApplyFlip()
    {
        if (sr) sr.flipX = movingLeft;               // if default art faces right
        else
        {
            Vector3 s = baseScale;
            s.x = Mathf.Abs(baseScale.x) * (movingLeft ? -1f : 1f);
            transform.localScale = s;
        }
    }
}