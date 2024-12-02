using BTAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostBehavior : MonoBehaviour
{
    public Transform target; // player character
    public float teleRange = 1.0f;
    public float followRange = 10.0f;
    public float wanderRadius = 5.0f;
    public Transform[] teleports; // places to teleport the character to
    public bool canTeleport = false; // flag to ensure no multiple teleportts
    // public Animator ghostAnimator;
    // public bool hasWaved = false; // flag to ensure no excessive waving

    private NavMeshAgent agent;
    private Root m_btRoot;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        m_btRoot = BT.Root();
        BTNode teleport = BT.Sequence()
            .OpenBranch(
            BT.Condition(() => InRange(teleRange)),
            BT.RunCoroutine(TeleBehavior));
        BTNode follow = BT.Sequence()
            .OpenBranch(
            BT.Condition(() => InRange(followRange) && !InRange(teleRange)),
            BT.RunCoroutine(FollowBehavior));
        BTNode wander = BT.Sequence()
            .OpenBranch(
            BT.Condition(() => !InRange(followRange)),
            BT.RunCoroutine(WanderBehavior));

        Selector selector = BT.Selector();
        selector.OpenBranch(teleport, follow, wander);
        m_btRoot.OpenBranch(selector);
    }

    // Update is called once per frame
    void Update()
    {
        // tick each frame!!
        m_btRoot.Tick();
    }

    private IEnumerator<BTState> TeleBehavior()
    {
        agent.ResetPath();
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= teleRange)
        {
            // hasWaved = true;
            // ghostAnimator.SetTrigger("Wave");
            
            canTeleport = true;
            int randomIndex = Random.Range(0, teleports.Length);
            Debug.Log("Teleporting Player to: " + teleports[randomIndex].position);
            Vector3 destination = teleports[randomIndex].position;

            // set teleport flag
            target.GetComponent<PlayerCharacter>().isTeleporting = true;

            // teleport player
            target.transform.position = destination;

            // reset teleport flag
            target.GetComponent<PlayerCharacter>().isTeleporting = false;
            canTeleport = false;
            // hasWaved = false;
        }
        yield return BTState.Success;
    }

    private IEnumerator<BTState> FollowBehavior()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= followRange)
        {
            // set target as destination when in range
            Debug.Log("Setting destination to: " + target.position);
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
        Vector3 randomDir = Random.insideUnitSphere * wanderRadius;
        randomDir += transform.position;

        NavMeshHit hit;
        // find valid point inside radius
        if (NavMesh.SamplePosition(randomDir, out hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            Debug.Log("Wandering to: " + hit.position);
        }
        else
        {
            Debug.Log("No valid wander destination found.");
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
}
