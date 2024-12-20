using BTAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BearBehavior : MonoBehaviour
{
    public Transform target; // player character
    public float attackRange = 1.0f;
    public float followRange = 10.0f;
    public float wanderRadius = 5.0f;
    public Animator bearAnimator;
    public bool canAttack = true;
    public GameObject nayScreen; // nayEndScreen

    private NavMeshAgent agent;
    private Root m_btRoot;

    // Start is called before the first frame update
    void Start()
    {
        bearAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        m_btRoot = BT.Root();
        if (m_btRoot == null)
        {
            Debug.LogError("Behavior Tree Root is null!");
            return;
        }
        BTNode attack = BT.Sequence()
            .OpenBranch(
            BT.Condition(() => InRange(attackRange) && canAttack),
            BT.RunCoroutine(AttackBehavior));
        BTNode follow = BT.Sequence()
            .OpenBranch(
            BT.Condition(() => InRange(followRange) && !InRange(attackRange)),
            BT.RunCoroutine(FollowBehavior));
        BTNode wander = BT.Sequence()
            .OpenBranch(
            BT.Condition(() => !InRange(followRange)),
            BT.RunCoroutine(WanderBehavior));

        Selector selector = BT.Selector();
        selector.OpenBranch(attack, follow, wander);
        m_btRoot.OpenBranch(selector);
    }

    // Update is called once per frame
    void Update()
    {
        // tick each frame!!
        m_btRoot.Tick();
    }

    private IEnumerator<BTState> AttackBehavior()
    {
        agent.ResetPath();
        bearAnimator.SetTrigger("isTilting");
        canAttack = true;
        if (canAttack)
        {
            Debug.Log("Attacking");
            EndGame();
            canAttack = false;
        }
        yield return BTState.Success;
    }

    private IEnumerator<BTState> FollowBehavior()
    {
        if (!canAttack)
        {
            canAttack = true;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= followRange)
        {
            agent.ResetPath();

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

    private IEnumerator<BTState> WanderBehavior()
    {
        if (!canAttack)
        {
            canAttack = true;
        }

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

    private bool InRange(float range)
    {
        bool inRange = Vector3.Distance(transform.position, target.position) <= range;
        return inRange;
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        nayScreen.SetActive(true);
    }
}
