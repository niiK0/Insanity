using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    public GameObject enemyWhoShot;
    private Vector3 moveDirection;
    private Rigidbody rb;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").transform;
        moveDirection = (player.position - transform.position).normalized * projectileSpeed;
        rb.velocity = new Vector3(moveDirection.x, 0, moveDirection.z);
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            player.gameObject.GetComponent<SimpleHealth>().TakeDamage(enemyWhoShot);
            Destroy(gameObject);
        }
    }
}
