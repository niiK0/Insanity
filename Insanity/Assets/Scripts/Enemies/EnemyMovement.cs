using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StatSystem;
using UnityEngine.Animations;

public class EnemyMovement : MonoBehaviour
{
    //private Rigidbody rb;

    private float speed => m_StatController.stats["Speed"].value;

    [SerializeField] private NavMeshAgent agent;

    private Transform player;
    public bool isMoving = false;
    [SerializeField] private Animator anim;


    protected StatController m_StatController;

    protected virtual void Awake()
    {
        m_StatController = GetComponent<StatController>();
    }

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = speed;
    }

    private void Update()
    {
        if(transform.gameObject.tag == "Boss")
        {
            anim.SetFloat("speed", agent.speed / 10);
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            agent.SetDestination(player.position);
        }

        if(agent.velocity != Vector3.zero)
        {
            isMoving = true;
            anim.SetBool("isMoving", true);
        }
        else
        {
            isMoving = false;
            anim.SetBool("isMoving", false);
        }
    }
}
