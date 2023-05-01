using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool isAttacking = false;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform shootPosition;

    private float attackInternalCw;
    [SerializeField] float attackCw;

    // Start is called before the first frame update
    void Start()
    {
        attackInternalCw = attackCw;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackInternalCw <= 0)
        {
            attackInternalCw = attackCw;
            Attack();
        }

        if(attackInternalCw >= 0)
        {
            attackInternalCw += -1 * Time.deltaTime;
        }
    }

    void Attack()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPosition.position, Quaternion.identity);
    }
}
