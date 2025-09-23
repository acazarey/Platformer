using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SidetoSide : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementDistance = 2f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool movingLeft = true;

    [Header("Kill Player On Contact")]
    [SerializeField] private bool useTrigger = true;     // set true if enemy collider OR player hurtbox is a Trigger
    [SerializeField] private string playerTag = "Player";

    private float leftEdge, rightEdge;
    private SpriteRenderer sr;
    private Vector3 baseScale;

    private void Awake()
    {
        baseScale = transform.localScale;
        sr = GetComponentInChildren<SpriteRenderer>();

        float x0 = transform.position.x;
        leftEdge  = x0 - movementDistance;
        rightEdge = x0 + movementDistance;

        // Ensure collider setting matches how we detect contact
        var col = GetComponent<Collider2D>();
        if (col) col.isTrigger = useTrigger;

        ApplyFlip();
    }

    private void Update()
    {
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

    // -------- Contact → kill player --------
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!useTrigger) return;
        if (!other.CompareTag(playerTag)) return;
        PlayerState.Instance.ChangeState(State.Death);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (useTrigger) return;
        if (!col.collider.CompareTag(playerTag)) return;
        PlayerState.Instance.ChangeState(State.Death);
    }
    // ---------------------------------------

    private void ApplyFlip()
    {
        if (sr) sr.flipX = movingLeft; // if art faces right by default
        else
        {
            var s = baseScale;
            s.x = Mathf.Abs(baseScale.x) * (movingLeft ? -1f : 1f);
            transform.localScale = s;
        }
    }
}
