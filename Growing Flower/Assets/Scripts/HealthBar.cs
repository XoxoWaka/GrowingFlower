using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image health;
    private float healthValue;
    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
}
