using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject respawnPoint;
    
    private void OnEnable()
    {
        PlayerState.OnStateChanged += Kill;
    }
    
    private void OnDisable()
    {
        PlayerState.OnStateChanged -= Kill;
    }

    private void Start()
    {
        Respawn();
    }

    private void Kill(State state)
    {
        if (state != State.Death)
            return;
        
        deathScreen.SetActive(true);
    }

    public void Respawn()
    {
        deathScreen.SetActive(false);
        PlayerState.Instance.ChangeState(State.Alive);
        Time.timeScale = 1;
        PointManager.Instance.ResetPoints();
        
        GoToSpawn();
    }

    public void GoToSpawn()
    {
        player.transform.position = respawnPoint.transform.position;
    }
    
    
}