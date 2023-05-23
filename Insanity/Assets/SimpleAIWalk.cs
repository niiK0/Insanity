using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleAIWalk : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update()
    {
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(target.position);
        }
    }
}
