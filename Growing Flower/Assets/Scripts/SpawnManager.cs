using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] thingPrefabs;
    public GameObject[] allActiveObjectsInScene;
    public GameObject bubble;
    public GameObject powerUp;

    public bool bubbleBool = true;
    public bool obstacleBool = true;
    public bool powerUpBool = true;

    public float obstacleSpawnTime = 1f;
    public float bubbleSpawnTime = 7.5f;
    public float powerUpSpawnTime = 50f;    

    private Vector3 obstacleSpawnPos;
    private Vector3 staffSpawnPos;
    private int index;    
    private float rangeX = 2.2f;
    private float startStaffTime = 5f;

    private void Start()
    {
        InvokeRepeating("PowerUpSpawn", 30f, 30f);

        bubbleBool = false;        
        StartCoroutine(startStaffCoroutine());
    }

    private void Update()
    { 
        if (obstacleBool)
        {
            obstacleSpawnPos = new Vector3(Random.Range(-rangeX, rangeX), transform.position.y, transform.position.z);
            index = Random.Range(0, thingPrefabs.Length);
            Instantiate(thingPrefabs[index], obstacleSpawnPos, gameObject.transform.rotation);
            obstacleBool = false;
            StartCoroutine(ObstacleSpawn());
        }
        if (bubbleBool)
        {
            staffSpawnPos = new Vector3(Random.Range(-rangeX, rangeX), transform.position.y, transform.position.z);
            Instantiate(bubble, staffSpawnPos, gameObject.transform.rotation);
            bubbleBool = false;
            StartCoroutine(BubbleSpawn());
        }        
    }

    private void PowerUpSpawn()
    {
        Instantiate(powerUp, staffSpawnPos, gameObject.transform.rotation);
    }


    IEnumerator ObstacleSpawn()
    {
        yield return new WaitForSeconds(obstacleSpawnTime);
        obstacleBool = true;
    }

    IEnumerator BubbleSpawn()
    {
        yield return new WaitForSeconds(bubbleSpawnTime);
        bubbleBool = true;        
    }    

    IEnumerator startStaffCoroutine()
    {
        yield return new WaitForSeconds(startStaffTime);
        bubbleBool = true;
        powerUpBool = true;
    }
}
