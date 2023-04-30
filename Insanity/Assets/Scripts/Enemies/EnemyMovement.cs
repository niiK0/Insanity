using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;

    private float speed = 4;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 direction = player.transform.position - rb.transform.position;
        direction.Normalize();
        rb.velocity = direction * speed;
    }
}
