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
    public float attackDist;


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
        agent.updateRotation = false;
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

        transform.rotation = (Quaternion.RotateTowards(transform.rotation, player.rotation, -220 * Time.deltaTime));

            
        if (Vector3.Distance(transform.position, player.position) >= attackDist)
        {
            agent.SetDestination(player.position);
            isMoving = true;
            anim.SetBool("isMoving", true);
        }
        else
        {
            agent.SetDestination(transform.position);
            isMoving = false;
            anim.SetBool("isMoving", false);
        }
    }
}
