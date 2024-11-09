using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public NavMeshAgent npc;
    public float wanderRadius = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // get nav mesh agent!!
        if (npc == null)
        {
            npc = GetComponent<NavMeshAgent>();
        }
        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        // check if npc has reached destination
        if (!npc.pathPending && npc.remainingDistance <= npc.stoppingDistance)
        {
            SetNewDestination();
        }
    }

    void SetNewDestination()
    {
        Vector3 randomDir = Random.insideUnitSphere * wanderRadius;
        randomDir += transform.position;

        NavMeshHit hit;
        // find valid point inside radius
        if (NavMesh.SamplePosition(randomDir, out hit, wanderRadius, NavMesh.AllAreas))
        {
            npc.SetDestination(hit.position);
        }
    }
}
