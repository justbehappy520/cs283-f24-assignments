using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BTAI;

public class AttackBehavior : MonoBehaviour
{
    public Transform target;
    public float attackRange = 5f;
    private Root m_btRoot = BT.Root();

    // Start is called before the first frame update
    void Start()
    {
        BTNode attack = BT.RunCoroutine(AttackPlayer);
        Sequence sequence = BT.Sequence();
        sequence.OpenBranch(attack);
        m_btRoot.OpenBranch(sequence);
    }

    // Update is called once per frame
    void Update()
    {
        m_btRoot.Tick();
    }

    public IEnumerator<BTState> AttackPlayer()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= attackRange)
        {
            agent.ResetPath();
            Debug.Log("Attack");
            yield return BTState.Continue;
        }
        yield return BTState.Success;
    }
}
