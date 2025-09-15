using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointUI : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI pointText;
    
    private void OnEnable()
    {
        PointManager.OnPointChanged += UpdateUI;
    }
    
    private void OnDisable()
    {
        PointManager.OnPointChanged -= UpdateUI;
    }


    private void UpdateUI(int amount)
    {
        pointText.text = amount.ToString();
    }
}
