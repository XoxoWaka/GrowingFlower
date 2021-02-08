using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    [SerializeField] private Manager managerScript; //манагер скрипт для изменения общей скорости
    [SerializeField] private PlayerController playerControllerScript; //для изменения скорости игрока после столкновения
    [SerializeField] private SpawnManager spawnManagerScript;
    [SerializeField] private Water waterScript;
    [SerializeField] private float waterBubble = 10; //количество воды в пузырьке
    private GameObject[] obstaclesArr; //содержит все препятсвия находящиеся на сцене
    private GameObject staff; //содержит штуки находящиеся на сцене
    private GameObject powerUp;
    private bool collisionSpeedBool = true;   //для переключения режима скорости после столкновения
    private bool powerUpBool = true;
    private float powerUpSpeed = 40; //скорость всего после PowerUp;
    private float managerSpeed; //изначальная скорость из манагера
    private float speedAfterCollision = 1; //скорость всех объектов после столкновения
    private float playerSpeedAfterCollision = 3; 
    private float playerSpeed; //изначальная скорость игрока
    private float collisionTime = 2.5f;
    private float powerUpTime = 5f;

    private void Start()
    {
        managerSpeed = GameObject.Find("Main Camera").GetComponent<Manager>().speed;        
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();        
        playerSpeed = playerControllerScript.speed;
        waterScript = GetComponent<Water>();

    }

    private void Update()
    {
        obstaclesArr = GameObject.FindGameObjectsWithTag("Obstacle");
        staff = GameObject.FindGameObjectWithTag("Staff");
        powerUp = GameObject.FindGameObjectWithTag("PowerUp");
    }

    private void OnTriggerEnter(Collider other)
    {
        //касание с POWERUP
        if (other.gameObject.CompareTag("PowerUp") && powerUpBool)
        {
            Destroy(other.gameObject);
            powerUpBool = false;
            collisionSpeedBool = false;
            managerScript.speed = powerUpSpeed;

            //логика для объектов на сцене
            for (int i = 0; i < obstaclesArr.Length; i++)
            {
                if (obstaclesArr != null) //а вдруг в момент собирания powerUp на сцене не будет камней
                {
                    obstaclesArr[i].GetComponent<MoveDown>().speed = powerUpSpeed;
                }                
            }
            if (staff != null) //если нет объекта Staff то просто пропускается эта логика
            {
                staff.GetComponent<MoveDown>().speed = powerUpSpeed;
            }

            StartCoroutine(PowerUpCoroutine());
            
        }

        //касание с пузырьком
        if (other.gameObject.CompareTag("Staff"))
        {
            Destroy(other.gameObject);
            waterScript.water += waterBubble;
        }

        //касание с препятствиями
        if (other.gameObject.CompareTag("Obstacle") && collisionSpeedBool)
        {
            playerControllerScript.speed = playerSpeedAfterCollision;
                
            //логика для создаваемых объектов
            managerScript.speed = speedAfterCollision;
            waterScript.water -= 20;
            collisionSpeedBool = false;
                        
            //логика для объектов на сцене
            for (int i = 0; i < obstaclesArr.Length; i++)
            {
                obstaclesArr[i].GetComponent<MoveDown>().speed = speedAfterCollision;
            }
            if (powerUp != null) //если нет объекта PowerUp то просто пропускается эта логика
            {
                powerUp.GetComponent<MoveDown>().speed = speedAfterCollision;
            }
            if (staff != null) //если нет объекта Staff то просто пропускается эта логика
            {
                staff.GetComponent<MoveDown>().speed = speedAfterCollision;
            }



            spawnManagerScript.bubbleSpawnTime = 15;
            spawnManagerScript.obstacleSpawnTime = 3;

            StartCoroutine(ReturnSpeedAfterCollision());
               
        }
    }

    //корутина для столкновений с препятствиями
    IEnumerator ReturnSpeedAfterCollision()
    {
        yield return new WaitForSeconds(collisionTime);
        collisionSpeedBool = true;
        playerControllerScript.speed = playerSpeed;
        managerScript.speed = managerSpeed;

        spawnManagerScript.bubbleSpawnTime = 10;
        spawnManagerScript.obstacleSpawnTime = 1;

        for (int i = 0; i < obstaclesArr.Length; i++)
        {
            obstaclesArr[i].GetComponent<MoveDown>().speed = managerSpeed;
        }
        if (powerUp != null) //если нет объекта PowerUp то просто пропускается эта логика
        {
            powerUp.GetComponent<MoveDown>().speed = managerSpeed;
        }
        if (staff != null)
        {
            staff.GetComponent<MoveDown>().speed = managerSpeed;
        }
    }

    IEnumerator PowerUpCoroutine()
    {
        yield return new WaitForSeconds(powerUpTime);
        managerScript.speed = managerSpeed;
        powerUpBool = true;
        collisionSpeedBool = true;

        //логика для объектов на сцене
        for (int i = 0; i < obstaclesArr.Length; i++)
        {
            if (obstaclesArr != null) //а вдруг в момент собирания powerUp на сцене не будет камней
            {
                obstaclesArr[i].GetComponent<MoveDown>().speed = managerSpeed;
            }
        }
        if (staff != null) //если нет объекта Staff то просто пропускается эта логика
        {
            staff.GetComponent<MoveDown>().speed = managerSpeed;
        }
    }
}
