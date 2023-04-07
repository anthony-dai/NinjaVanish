using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float difficulty = 1;
    public bool flashLightOn = true;
    public float walkSpeed = 2.5f;
    public float sprintSpeed = 7.5f;
    public float turnSpeed = 5.0f;
    public int timesSpotted = 0;
    // public int alertnessLevel = 1;
    public LayerMask obstructionMask;

    public float radiusFOV = 10;
    public float angleFOV = 90;

    public float wanderRadius = 1f;
    public float wanderDistance = 3f;
    public float wanderJitter = 5f;

    [HideInInspector] EnemiesControl enemiesControl;
    private void Awake()
    {
        setSpeed(difficulty);
        setFOV(difficulty);
    }
    private void Start()
    {
        enemiesControl = GameObject.Find("EnemiesControl").GetComponent<EnemiesControl>();
        
        if (!flashLightOn)
        {
            GameObject flashLight = transform.GetChild(2).gameObject;
            flashLight.SetActive(false);
        }
    }
    private void Update()
    {
        if (timesSpotted >= 10) enemiesControl.moreAlert = true;
    }

    public void setSpeed(float diff)
    {
        walkSpeed *= diff;
        sprintSpeed *= diff;
    }

    public void setFOV(float diff)
    {
        radiusFOV *= diff;
        angleFOV *= diff;
    }
}
