using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyHandMovement : MonoBehaviour
{
    public Transform leftHand;
    public Vector3 offset;

    private void Start()
    {
        offset = transform.position - leftHand.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = leftHand.position + offset;
    }
}
