using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerref : MonoBehaviour
{
    public Transform playerPos;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        transform.position = playerPos.position;
    }
}
