using System.Collections;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject respawnPoint;

    [Header("Animation")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private string deathStateName = "death";
    [SerializeField] private string deathTriggerName = "die";

    [Header("Optional Locks")]
    [SerializeField] private MonoBehaviour[] componentsToDisable;
    [SerializeField] private Collider2D[] collidersToDisable;
    [SerializeField] private Rigidbody2D rb;

    private bool skipAnimationOnce = false;

    private void OnEnable()  { PlayerState.OnStateChanged += Kill; }
    private void OnDisable() { PlayerState.OnStateChanged -= Kill; }

    public void InstantKill()
    {
        skipAnimationOnce = true;
        PlayerState.Instance.ChangeState(State.Death);
    }

    private void Kill(State state)
    {
        if (state != State.Death) return;

        foreach (var c in componentsToDisable) if (c) c.enabled = false;
        foreach (var col in collidersToDisable) if (col) col.enabled = false;
        if (rb) { rb.velocity = Vector2.zero; rb.simulated = false; }

        if (skipAnimationOnce || playerAnimator == null)
        {
            skipAnimationOnce = false;
            deathScreen.SetActive(true);
            Time.timeScale = 0f;
            return;
        }

        StartCoroutine(PlayDeathThenPause());
    }

    private IEnumerator PlayDeathThenPause()
    {
        playerAnimator.ResetTrigger(deathTriggerName);
        playerAnimator.SetTrigger(deathTriggerName);
        yield return null;

        int frames = 0;
        var info = playerAnimator.GetCurrentAnimatorStateInfo(0);
        while (!info.IsName(deathStateName) && frames++ < 60)
        {
            yield return null;
            info = playerAnimator.GetCurrentAnimatorStateInfo(0);
        }

        while (playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;

        deathScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Awake()
    {
    if (!playerAnimator && player)
        playerAnimator = player.GetComponentInChildren<Animator>();
    }

    public void Respawn()
    {
        if (deathScreen) deathScreen.SetActive(false);
        PlayerState.Instance.ChangeState(State.Alive);

        if (player && respawnPoint)
            player.transform.position = respawnPoint.transform.position;

        foreach (var c in componentsToDisable) if (c) c.enabled = true;
        foreach (var col in collidersToDisable) if (col) col.enabled = true;
        if (rb) rb.simulated = true;

        Time.timeScale = 1f;
    }
}
