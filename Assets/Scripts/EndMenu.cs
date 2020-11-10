using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;


    private void OnEnable()
    {
        score.text = $"{score.text} {PlayerStats.Points}";
    }
}
