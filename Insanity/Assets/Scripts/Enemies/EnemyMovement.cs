using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 300;
    private Vector3 currDirection;

    private float stdMoveTimer = 0;
    private float stdMoveDuration = 2;
    private float stdStopDuration = 2.5f;
    private bool stdIsMoving = false;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        UpdateTimer();

        if (stdIsMoving){
            Move(currDirection);
        } else {
            rb.velocity = Vector3.zero;
        }
    }

    private Vector3 GetRandomDir()
    {
        Vector2 newDir = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
        newDir = newDir.normalized;
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
        }

        if (!stdIsMoving) {
            if (stdMoveTimer >= stdStopDuration)
            {
                stdMoveTimer = 0;
                stdIsMoving = true;
                currDirection = GetRandomDir();
            }
        }
        
    }

    private void Move(Vector3 Dir)
    {
        rb.velocity = Dir * speed * Time.deltaTime;
    }
}
