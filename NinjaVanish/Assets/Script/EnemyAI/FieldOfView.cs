using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public EnemyStats enemyStats;
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;
    public LightInfo lightInfo;
    public StateController controller;
    public Transform fovRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public LayerMask smokeobstructionMask;
    // public EnemyBehaviour enemyBehaviour;

    public bool canSeePlayer;

    private void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        lightInfo = playerRef.GetComponent<LightInfo>();
        targetMask = LayerMask.GetMask("Player");
        smokeobstructionMask = LayerMask.GetMask("smokeObstruction");
        obstructionMask = LayerMask.GetMask("Obstruction");
        controller = GetComponent<StateController>();
        radius = enemyStats.radiusFOV;
        angle = enemyStats.angleFOV;
        StartCoroutine(FOVRoutine());

    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(fovRef.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position + Vector3.up - fovRef.position).normalized;
            if (Vector3.Angle(fovRef.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(fovRef.position, target.position);

                if (!Physics.Raycast(fovRef.position, directionToTarget, distanceToTarget, obstructionMask) && (lightInfo.InLight|| controller.enemyStats.flashLightOn) && !Physics.Raycast(fovRef.position, directionToTarget, distanceToTarget, smokeobstructionMask))
                {
                    canSeePlayer = true;
                    controller.enemyStats.timesSpotted += 1;
                    controller.IsDetected = true;
                }
                else
                {
                    canSeePlayer = false;
                    controller.IsDetected = false;
                }
            }
            else
            {
                canSeePlayer = false;
                controller.IsDetected = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = true;
            controller.IsDetected = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag is "smoketag")
        {
            // if walking in smoke, cannot see player
            Debug.Log("in the smoke");
            radius = 1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag is "smoketag")
        {
            // if walking in smoke, cannot see player
            Debug.Log("Not in the smoke anymore");
            radius = 10;
        }
    }
}
