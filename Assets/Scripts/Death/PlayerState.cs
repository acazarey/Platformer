using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance {get; private set;}
    
    public static event Action<State> OnStateChanged;
    public State CurrentState { get; private set; } = State.Alive;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void ChangeState(State newState)
    {
        CurrentState = newState;
        OnStateChanged?.Invoke((State)CurrentState);
    }
}

public enum State
{
    Death,
    Alive
}
