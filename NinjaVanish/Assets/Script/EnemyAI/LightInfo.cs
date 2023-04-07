using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInfo : MonoBehaviour
{
    [HideInInspector] public LayerMask obstructionMask;

    // Other functions can only read the inLight boolean
    private bool inLight; // field
    public bool InLight // property
    {
        get { return inLight; }   // get method
    }

    // Start is called before the first frame update
    void Start()
    {
        obstructionMask = LayerMask.GetMask("Obstruction");
        inLight = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("LightCollider"))
        {
            Vector3 directionToTarget = other.transform.position - transform.position;
            float distanceToTarget = directionToTarget.magnitude;
            if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
            {
                Debug.DrawRay(transform.position, directionToTarget, Color.red);
                inLight = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("LightCollider"))
        {
            inLight = false;
        }
    }
}
