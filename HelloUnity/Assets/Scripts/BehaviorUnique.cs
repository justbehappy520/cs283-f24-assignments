using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTAI;
using UnityEngine.AI;

public class BehaviorUnique : MonoBehaviour
{
    public Transform target;
    public float quoteRange = 15.0f;
    public float fleeRange = 30.0f;
    public float wanderRadius = 10.0f;

    private bool isQuoting = false;

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
            "The journey of a thousand miles begins with one step. – Lao Tzu",
            "Don't cry because it's over, smile because it happened. – Dr. Seuss",
            "To live is the rarest thing in the world. Most people exist, that is all. – Oscar Wilde",
            "The purpose of life is not to be happy. It is to be useful, to be honorable, to be compassionate, to have it make some difference that you have lived and lived well. – Ralph Waldo Emerson",
            "It always seems impossible until it's done. – Nelson Mandela",
            "The unexamined life is not worth living. – Socrates",
            "What you get by achieving your goals is not as important as what you become by achieving your goals. – Zig Ziglar",
            "Success usually comes to those who are too busy to be looking for it. – Henry David Thoreau",
            "I think, therefore I am. – René Descartes",
            "An unexamined life is a life not worth living. – Socrates",
            "We are all in the gutter, but some of us are looking at the stars. – Oscar Wilde",
            "It is never too late to be what you might have been. – George Eliot",
            "To love and be loved is to feel the sun from both sides. – David Viscott",
            "Not how long, but how well you have lived is the main thing. – Seneca",
            "You only live once, but if you do it right, once is enough. – Mae West",
            "You miss 100% of the shots you don’t take. – Wayne Gretzky"
        };

    // Start is called before the first frame update
    void Start()
    {
        npcRB = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        m_btRoot = BT.Root();
        BTNode quote = BT.Sequence()
            .OpenBranch(
            BT.Condition(() => InRange(quoteRange)),
            BT.RunCoroutine(QuoteBehavior));
        BTNode flee = BT.Sequence()
            .OpenBranch(
            BT.Condition(() => InRange(fleeRange) && !InRange(quoteRange)),
            BT.RunCoroutine(FleeBehavior));
        BTNode wander = BT.Sequence()
            .OpenBranch(
            BT.Condition(() => !InRange(fleeRange)),
            BT.RunCoroutine(WanderBehavior));

        Selector selector = BT.Selector();
        selector.OpenBranch(quote, flee, wander);
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
        Debug.Log("Ahhhhh");
        float distance = Vector3.Distance(transform.position, target.position);

        while (distance <= fleeRange)
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
        while (isQuoting && quotes.Length > 0)
        {
            string randomQuote = quotes[Random.Range(0, quotes.Length)];
            Debug.Log(randomQuote);
            //yield return BTState.Continue;
        }

        isQuoting = false;
        yield return BTState.Success;
    }

    private bool InRange(float range)
    {
        bool inRange = Vector3.Distance(transform.position, target.position) <= range;
        return inRange;
    }
}
