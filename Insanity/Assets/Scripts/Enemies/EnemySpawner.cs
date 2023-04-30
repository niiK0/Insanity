using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float radius;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Spawn a new enemy
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * radius;
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }
}
