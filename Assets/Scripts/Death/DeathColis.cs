using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathColis : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            PlayerState.Instance.ChangeState(State.Death);
        }
    }
}