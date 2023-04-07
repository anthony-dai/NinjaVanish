using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory: MonoBehaviour
{

    Throwscript throwscript;
    LineRenderer lineRenderer;


    public Transform Handposition;

    public int numPoints = 50;

    public float timeBetweenPoints = 0.1f;

    public LayerMask CollidableLayers;

    private void Start()
    {
        throwscript = GetComponent<Throwscript>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (throwscript.throwing == true)
        {
            lineRenderer.enabled = true;

            lineRenderer.positionCount = numPoints;
            List<Vector3> points = new List<Vector3>();
            Vector3 startingPosition = Handposition.position;
            Vector3 startingVelocity = Handposition.transform.up * throwscript.CalculateHoldDownForce(throwscript.holdDownTime);

            for (float t = 0; t < numPoints; t += timeBetweenPoints)
            {
                Vector3 newPoint = startingPosition + t * startingVelocity;
                newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t; // y = y0 + v0*t - 1/2*g*t^2
                points.Add(newPoint);

                // to stop the line from drawing
                if (Physics.OverlapSphere(newPoint, 0.1f, CollidableLayers).Length > 0.0)
                {
                    lineRenderer.positionCount = points.Count;
                    break;
                }

            }

            lineRenderer.SetPositions(points.ToArray());
        }

        else
        {
            lineRenderer.enabled = false;
        }
    
    }

}
