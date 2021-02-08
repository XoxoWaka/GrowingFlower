using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20;
    public float horizontalInput;    
    private float XRange = 2.6f;

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");        
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);        

        if (transform.position.x > XRange)
        {
            transform.position = new Vector3(XRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -XRange)
        {
            transform.position = new Vector3(-XRange, transform.position.y, transform.position.z);
        }

    }
}
