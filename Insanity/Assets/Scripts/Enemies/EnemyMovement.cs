using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private const float slowSpeed = 300;
    private const float fastSpeed = 300;
    private const float rotateSpeed = 30;

    [SerializeField] private Vector3 currDirection;

    private float stdMoveTimer = 0;
    private float stdMoveDuration = 2;
    private float stdStopDuration = 2.5f;
    [SerializeField] private bool stdIsMoving = false;

    private float alertRadius = 1000;
    [SerializeField] private bool playerInRange = false;

    private Rigidbody rb;
    [SerializeField] private GameObject player;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        CheckForPlayer();

        if (playerInRange)
        {
            currDirection = player.transform.position - transform.position;
            Move(currDirection, fastSpeed);
        }
        else
        {
            UpdateTimer();

            if (stdIsMoving){
                Move(currDirection, slowSpeed);
            } else {
                rb.velocity = Vector3.zero;
            }
        }
    }

    private Vector3 GetRandomDir()
    {
        Vector2 newDir = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
        return new Vector3(newDir.x, 0, newDir.y);
    }

    private void UpdateTimer()
    {
        stdMoveTimer += Time.deltaTime;

        if (stdIsMoving){
            if (stdMoveTimer >= stdMoveDuration){
                stdMoveTimer = 0;
                stdIsMoving = false;
            }
        } else {
            if (stdMoveTimer >= stdStopDuration)
            {
                stdMoveTimer = 0;
                stdIsMoving = true;
                currDirection = GetRandomDir();
            }
        }
        
    }

    private void Move(Vector3 Dir, float speed)
    {
        Vector3 dir = Dir.normalized;
        Debug.Log("Normalizado");
        dir.y = 0;
        //transform.forward = Vector3.Slerp(transform.forward, -dir, Time.deltaTime * rotateSpeed);
        rb.velocity = dir * speed * Time.deltaTime;
        Debug.Log("Me movi");
    }

    private void CheckForPlayer()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = transform.position;
        if ((enemyPos - playerPos).magnitude < alertRadius) {
            playerInRange = true;
            Debug.Log("achei");
        } else {
            playerInRange = false;
        }
    }
}
