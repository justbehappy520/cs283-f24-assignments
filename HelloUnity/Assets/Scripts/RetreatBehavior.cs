using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTAI;

public class RetreatBehavior : MonoBehaviour
{
    private Vector3 home;
    private Root m_btRoot = BT.Root();

    // Start is called before the first frame update
    void Start()
    {
        home = transform.position;
        BTNode retreat = BT.RunCoroutine(RunAway);
        Sequence sequence = BT.Sequence();
        sequence.OpenBranch(retreat);
        m_btRoot.OpenBranch(sequence);
    }

    // Update is called once per frame
    void Update()
    {
        m_btRoot.Tick();
    }

    public IEnumerator<BTState> RunAway()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(home);

        while(Vector3.Distance(transform.position, home) > 0.1f)
        {
            yield return BTState.Continue;
        }

        yield return BTState.Success;
    }
}
