using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTAI;
using UnityEngine.AI;

public class BehaviorUnique : MonoBehaviour
{
    public Transform target;
    public float quoteRange =30.0f;
    public float fleeRange = 10.0f;
    public float wanderRadius = 10.0f;

    private bool isQuoting = false;
    private bool hasQuoted = false;

    private Rigidbody npcRB;
    private NavMeshAgent agent;
    private Root m_btRoot;

    public string[] quotes = new string[]
        {
            "The only way to do great work is to love what you do. – Steve Jobs",
            "In the end, we will remember not the words of our enemies, but the silence of our friends. – Martin Luther King Jr.",
            "To be yourself in a world that is constantly trying to make you something else is the greatest accomplishment. – Ralph Waldo Emerson",
            "Success is not final, failure is not fatal: It is the courage to continue that counts. – Winston Churchill",
            "It does not matter how slowly you go as long as you do not stop. – Confucius",
            "Life is what happens when you're busy making other plans. – John Lennon",
            "The best way to predict your future is to create it. – Abraham Lincoln",
            "You must be the change you wish to see in the world. – Mahatma Gandhi",
            "Happiness is not something ready-made. It comes from your own actions. – Dalai Lama",
        };

    // Start is called before the first frame update
    void Start()
    {
        npcRB = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        m_btRoot = BT.Root();
        BTNode flee = BT.Sequence()
            .OpenBranch(
            BT.Condition(() => InRange(fleeRange)),
            BT.RunCoroutine(FleeBehavior));
        BTNode quote = BT.Sequence()
            .OpenBranch(
            BT.Condition(() => InRange(quoteRange) && !hasQuoted),
            BT.RunCoroutine(QuoteBehavior));
        BTNode unquote = BT.Sequence()
            .OpenBranch(
            BT.Condition(() => !InRange(quoteRange) && hasQuoted),
            BT.RunCoroutine(UnQuoteBehavior));
        BTNode wander = BT.Sequence()
            .OpenBranch(
            BT.Condition(() => !InRange(fleeRange)),
            BT.RunCoroutine(WanderBehavior));

        Selector selector = BT.Selector();
        selector.OpenBranch(flee, quote, unquote, wander);
        m_btRoot.OpenBranch(selector);
    }

    // Update is called once per frame
    void Update()
    {
        m_btRoot.Tick();
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
        }

        // wait for agent to reach destination
        while (agent.remainingDistance > 0.1f)
        {
            yield return BTState.Continue;
        }

        yield return BTState.Success;
    }

    private IEnumerator<BTState> FleeBehavior()
    {
        Debug.Log("Fleeing: Ahhhhh");
        float distance = Vector3.Distance(transform.position, target.position);

        while (InRange(fleeRange))
        {
            Vector3 away = transform.position - target.position;
            away.Normalize();
            agent.SetDestination(transform.position + away * wanderRadius);
            yield return BTState.Continue;
        }
        yield return BTState.Success;
    }

    private IEnumerator<BTState> QuoteBehavior()
    {
        isQuoting = true;
        if (isQuoting && quotes.Length > 0)
        {
            string randomQuote = quotes[Random.Range(0, quotes.Length)];
            Debug.Log(randomQuote);
            hasQuoted = true;
        }

        isQuoting = false;
        yield return BTState.Success;
    }

    private IEnumerator<BTState> UnQuoteBehavior()
    {
        if (hasQuoted)
        {
            hasQuoted = false;
        }
        yield return BTState.Success;
    }

    private bool InRange(float range)
    {
        bool inRange = Vector3.Distance(transform.position, target.position) <= range;
        return inRange;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Quoted");
            hasQuoted = false;
        }
    }
}
