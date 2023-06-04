using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float radius;
    private bool AlreadyChecked = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Debug.Log("Ola");
            // Spawn a new enemy
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * radius;
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

            //Already checked stops mobs from spawning again
            AlreadyChecked= true;
        }
    }
}
