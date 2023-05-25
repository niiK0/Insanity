using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private bool canAttack = false;
    [SerializeField] private GameObject handCollider;

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            Attack();
        }

        if (GetComponent<EnemyMovement>().isMoving == false)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }

    void Attack()
    {
        anim.SetTrigger("attack");
    }

    
    public void DisableEnemyHandCollider()
    {
        handCollider.GetComponent<Collider>().enabled = false;
    }

    public void EnableEnemyHandCollider()
    {
        handCollider.GetComponent<Collider>().enabled = true;
    }
}
