using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetect : MonoBehaviour
{
    //private bool seePlayer;
    //public bool SeePlayer
    //{ get; }
    public LayerMask obstructionMask;
    public LayerMask groundMask;
    public CapsuleCollider capsuleCollider;
    public Transform surCamera;
    public Light surCameraLight;
    public GameObject alertCollider;
    private bool inRange;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        //seePlayer = false;
        groundMask = LayerMask.GetMask("Ground");
        obstructionMask = LayerMask.GetMask("Obstruction");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        capsuleCollider = GetComponent<CapsuleCollider>();
        alertCollider = transform.GetChild(0).gameObject;
        alertCollider.SetActive(false);
        surCamera = transform.parent.transform.GetChild(2);
        surCameraLight = surCamera.GetComponent<Light>();
        surCameraLight.color = Color.green;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, groundMask);
        transform.position = hit.point;
        inRange = false;
    }

    private void Update()
    {
        if (inRange)
        {
            Vector3 target = player.position;
            Vector3 directionToTarget = (target - surCamera.position).normalized;
            float distanceToTarget = Vector3.Distance(surCamera.position, target);
            if (!Physics.Raycast(surCamera.position, directionToTarget, distanceToTarget, obstructionMask))
            {
                surCameraLight.color = Color.red;
                alertCollider.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            surCameraLight.color = Color.green;
            alertCollider.SetActive(false);
        }
    }
}
