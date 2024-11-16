using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTAI;

public class WanderBehavior : MonoBehaviour
{
    public float wanderRadius = 20f; // radius around npc
    private Root m_btRoot = BT.Root();

    // Start is called before the first frame update
    void Start()
    {
        BTNode moveTo = BT.RunCoroutine(WalkAround);

        Sequence sequence = BT.Sequence();
        sequence.OpenBranch(moveTo);

        m_btRoot.OpenBranch(sequence);
    }

    // Update is called once per frame
    void Update()
    {
        m_btRoot.Tick();
    }

    public IEnumerator<BTState> WalkAround()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Vector3 randomDir = Random.insideUnitSphere * wanderRadius;
        randomDir += transform.position;

        NavMeshHit hit;
        // find valid point inside radius
        if (NavMesh.SamplePosition(randomDir, out hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }

        // wait for agent to reach destination
        while (agent.remainingDistance > 0.1f)
        {
            yield return BTState.Continue;
        }

        yield return BTState.Success;
    }
}
