using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    private Transform Boss;
    public Transform spawnPoint;

    void Start()
    {
        Boss = GameObject.FindWithTag("Boss").transform;
        Spawn();
    }

    public void Spawn()
    {
        Boss.position = spawnPoint.position;
    }
}
