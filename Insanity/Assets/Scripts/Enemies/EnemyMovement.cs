using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatSystem;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody rb;

    private float speed => m_StatController.stats["Speed"].value;

    private bool isMoving = false;
    private float movementTimer = 0;
    private float timeStopped = 0.5f;
    private float timeMoving = 1f;

    private Vector3 currDirection;

    protected StatController m_StatController;

    protected virtual void Awake()
    {
        m_StatController = GetComponent<StatController>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        TimerUpdate();

        if (isMoving)
        {
            MoveEnemy();
        }
    }

    private void TimerUpdate()
    {
        movementTimer += Time.deltaTime;

        if (isMoving == true && movementTimer >= timeMoving)
        {
            isMoving = false;
            movementTimer = 0;
        }
        
        if (isMoving == false && movementTimer >= timeStopped)
        {
            isMoving = true;
            movementTimer = 0;
            currDirection = RandomDirection();
        }
    }

    private Vector3 RandomDirection()
    {
        Vector3 randomVec = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
        return randomVec;
    }

    private void MoveEnemy()
    {
        Vector3 dir = currDirection.normalized;
        rb.velocity = dir * speed;
    }
}
