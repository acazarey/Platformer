using UnityEngine;
using System.Collections;

public class PlayerAttackHitbox : MonoBehaviour
{
    [SerializeField] private Collider2D attackHitbox;  // assign the child collider
    [SerializeField] private float activeTime = 0.15f; // window the hitbox is active

    public void EnableAttack()  { if (attackHitbox) attackHitbox.enabled = true;  }
    public void DisableAttack() { if (attackHitbox) attackHitbox.enabled = false; }

    public IEnumerator AttackWindow()
    {
        EnableAttack();
        yield return new WaitForSeconds(activeTime);
        DisableAttack();
    }
}