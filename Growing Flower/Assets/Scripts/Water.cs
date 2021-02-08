using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Water : MonoBehaviour
{
    [SerializeField] private Image health; 
    [SerializeField] private float waterPerSecond = 1; 
    public float water = 100; 


    private void Update()
    {
        water -= waterPerSecond * Time.deltaTime;
        
        health.fillAmount = water / 100;

        if (water < 0)
        {
            Destroy(gameObject);
        }
        if (water > 100)
        {
            water = 100;
        }
    }    

}
