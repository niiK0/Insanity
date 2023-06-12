using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float radius;
    public int minAmount, maxAmount;
    private int amount;
    private bool hasSpawned = false;
    private Collider[] enemies;
    public ItemSpawner itemSpawner;
    public RightDoor rDoor;
    public LeftDoor lDoor;
    public UpDoor uDoor;
    public DownDoor dDoor;
    private bool finishedRoom = false;

    private Collider[] player;
    public bool currentRoom = false;
    public bool spawnExtraMobs = false;
    public int extraMinAmount, extraMaxAmount;
    private int extraAmount;

    private void OnDrawGizmos()
    {  
        //Gizmos.DrawSphere(transform.position, radius);
        Gizmos.DrawCube(transform.position, new Vector3(30,9,30));
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
    }

    private void Start()
    {
        amount = Random.Range(minAmount, maxAmount);
        extraAmount = Random.Range(extraMinAmount, extraMaxAmount);
    }

    private void Update()
    {
        enemies = Physics.OverlapBox(transform.position, new Vector3(30, 9, 30), Quaternion.identity, LayerMask.GetMask("Enemy"));
        player = Physics.OverlapBox(transform.position, new Vector3(30, 9, 30), Quaternion.identity, LayerMask.GetMask("Player"));

        currentRoom = player.Length == 1 ? true : false;

        if (hasSpawned && enemies.Length <= 0 && !finishedRoom)
        {
            finishedRoom = true;
            itemSpawner.RoomCleared = true;
            rDoor.MakeAbleToLeave();
            lDoor.MakeAbleToLeave();
            uDoor.MakeAbleToLeave();
            dDoor.MakeAbleToLeave();
        }

        if (spawnExtraMobs)
        {
            for (int i = 0; i < extraAmount; i++)
            {
                Vector3 randomPosition = transform.position + Random.insideUnitSphere * radius;
                Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            }
            spawnExtraMobs = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasSpawned = true;
            gameObject.GetComponent<Collider>().enabled = false;
            // Spawn a new enemy
            for (int i = 0; i < amount; i++)
            {
                Vector3 randomPosition = transform.position + Random.insideUnitSphere * radius;
                Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            }
        }
    }
}

