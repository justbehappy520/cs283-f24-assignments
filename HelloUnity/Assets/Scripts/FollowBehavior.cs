using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTAI;

public class FollowBehavior : MonoBehaviour
{
    public Transform target;
    public float followRange = 20f;
    private Root m_btRoot = BT.Root();

    // Start is called before the first frame update
    void Start()
    {
        BTNode follow = BT.RunCoroutine(FollowPlayer);
        Sequence sequence = BT.Sequence();
        sequence.OpenBranch(follow);
        m_btRoot.OpenBranch(sequence);
    }

    // Update is called once per frame
    void Update()
    {
        m_btRoot.Tick();
    }

    public IEnumerator<BTState> FollowPlayer()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        if (distance <= followRange)
        {
            // set target as destination when in range
            agent.SetDestination(target.position);
            yield return BTState.Continue;
        }
        else
        {
            // stop when target is out of range
            agent.ResetPath();
            yield return BTState.Success;
        }
    }
}
