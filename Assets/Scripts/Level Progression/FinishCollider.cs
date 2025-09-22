using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCollider : MonoBehaviour
{
    [SerializeField] private GameObject finishScreen;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            finishScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
