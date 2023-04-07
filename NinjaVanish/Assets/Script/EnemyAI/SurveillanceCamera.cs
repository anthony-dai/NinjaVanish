using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveillanceCamera : MonoBehaviour
{
    public float turnSpeed = 7.5f;
    public float maxRightAngle = 85f;
    public float maxLeftAngle = 85f;
    public bool isRotatable = true;
    private Vector2 leftBound;
    private Vector2 rightBound;
    private float speed;
    public LayerMask obstructionMask;
    public GameObject cameraDetect;
    public GameObject cameraLight;

    // Start is called before the first frame update
    void Start()
    {
        speed = turnSpeed;
        leftBound.Set(-transform.forward.z, transform.forward.x);
        rightBound.Set(transform.forward.z, -transform.forward.x);
        obstructionMask = LayerMask.GetMask("Obstruction");
        cameraDetect = transform.GetChild(1).gameObject;
        cameraLight = transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotatable)
        {
            Vector2 forwardDir = new Vector3(transform.forward.x, transform.forward.z);
            if (Vector2.Angle(forwardDir, leftBound) < 90f - maxLeftAngle) speed = turnSpeed;
            else if (Vector2.Angle(forwardDir, rightBound) < 90f - maxRightAngle) speed = -turnSpeed;
            transform.Rotate(0f, speed * Time.deltaTime, 0f, Space.World);
        }
    }

    //Disables the camera when hit by rock
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            cameraLight.SetActive(false);
            cameraDetect.SetActive(false);
            enabled = false;
        }
    }
}
