using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftRight : MonoBehaviour
{
    public Manager managerScript;
    public float speed;
    private float rangeX = 8;
    private Quaternion flipZ;
    private bool flipTranslate = true;

    private void Start()
    {
        managerScript = GameObject.Find("Main Camera").GetComponent<Manager>();
        speed = managerScript.speed;
        if (transform.position.x > 0)
        {
            flipTranslate = false;
        }
    }

    private void Update()
    {
        if (flipTranslate)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            
        }
        else if (!flipTranslate)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.Translate(Vector3.right * speed * Time.deltaTime);            
        }
        if (transform.position.x > rangeX || transform.position.x < -rangeX)
        {
            Destroy(gameObject);
        }

        
    }
}
