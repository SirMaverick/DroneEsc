using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshDrone : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform goal;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        agent.SetDestination(goal.position);
    }
}
