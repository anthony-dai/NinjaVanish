using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public State currentState;
    public State remainState;

    // Enemy parameters
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public int m_CurrentWaypointIndex;
    public EnemyStats enemyStats;

    // Investigation variables
    [HideInInspector] public Vector3 rightBound = Vector3.zero;
    [HideInInspector] public Vector3 leftBound = Vector3.zero;
    [HideInInspector] public bool lookAround;
    [HideInInspector] public int lookAroundCount;
    [HideInInspector] public Vector3 susLocation;
    //[HideInInspector] public bool doneWander;
    //[HideInInspector] public int wanderCount;
    public bool IsHeard
    { get; set; }
    public bool IsDetected
    { get; set; }

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyStats = GetComponent<EnemyStats>();
        rb = GetComponent<Rigidbody>();

        navMeshAgent.speed = enemyStats.walkSpeed;
        IsDetected = false;
        lookAround = false;
        IsHeard = false;
        lookAroundCount = 0;
        susLocation = Vector3.zero;
        //doneWander = false;
        //wanderCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnDrawGizmos()
    {
        if (currentState != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(transform.position, 1.0f);
        }
    }

    public void TransitionToNextState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
        }
    }
}
