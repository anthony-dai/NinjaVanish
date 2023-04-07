using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{
    public Animator enemyAnimator;
    public Rigidbody rb;
    public NavMeshAgent navMeshAgent;
    public EnemyStats enemyStats;
    public StateController controller;
    private bool isWalking;
    private bool isSprinting;
    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        enemyStats = GetComponent<EnemyStats>();
        controller = GetComponent<StateController>();
        isWalking = false;
        isSprinting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.lookAround)
        {
            isWalking = false;
            isSprinting = false;
        }
        else if (Mathf.Approximately(navMeshAgent.speed, enemyStats.walkSpeed))
        {
            isWalking = true;
            isSprinting = false;
        }
        else
        {
            isWalking = true;
            isSprinting = true;
        }
        enemyAnimator.SetBool("isWalking", isWalking);
        enemyAnimator.SetBool("isSprinting", isSprinting);
    }
}
