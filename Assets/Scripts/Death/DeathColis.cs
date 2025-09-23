using UnityEngine;

public class DeathColis : MonoBehaviour
{
    [SerializeField] private bool isFallZone = true;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private DeathManager deathManager;

    private void Awake()
    {
        if (!deathManager) deathManager = FindObjectOfType<DeathManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        if (isFallZone)
        {
            if (deathManager) deathManager.InstantKill();
            else PlayerState.Instance.ChangeState(State.Death);
        }
        else
        {
            PlayerState.Instance.ChangeState(State.Death);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.CompareTag(playerTag)) return;

        if (isFallZone)
        {
            if (deathManager) deathManager.InstantKill();
            else PlayerState.Instance.ChangeState(State.Death);
        }
        else
        {
            PlayerState.Instance.ChangeState(State.Death);
        }
    }
}
