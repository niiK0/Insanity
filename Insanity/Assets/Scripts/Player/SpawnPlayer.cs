using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    private Transform player;
    public Transform spawnPoint;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        Spawn();
    }

    public void Spawn()
    {
        player.position = spawnPoint.position;
    }
}
