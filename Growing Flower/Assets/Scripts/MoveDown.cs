using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public Manager managerScript;
    public float speed;
    private float downBound = -6f;

    private void Start()
    {
        managerScript = GameObject.Find("Main Camera").GetComponent<Manager>();
        speed = managerScript.speed;
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);

        if (transform.position.y < downBound)
        {
            Destroy(gameObject);
        }
    }    

}
