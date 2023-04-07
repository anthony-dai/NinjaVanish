using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Wander")]
public class WanderAction : Action
{
    public override void Act(StateController controller)
    {
        Wander(controller);
    }

    public void Wander(StateController controller)
    {
        Vector3 wanderTarget = Vector3.zero;
        wanderTarget += new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)) * controller.enemyStats.wanderJitter;
        wanderTarget.Normalize();
        wanderTarget *= controller.enemyStats.wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0f, 0f, controller.enemyStats.wanderDistance);
        Vector3 targetWorld = controller.transform.InverseTransformVector(targetLocal);
        
        // Make sure enemy doesn't try to check in an obstruction
        RaycastHit hit;
        Ray ray = new Ray(controller.transform.position, targetWorld - controller.transform.position);
        if (Physics.Raycast(ray, out hit, targetLocal.magnitude, controller.enemyStats.obstructionMask))
        {
            targetWorld.Set(hit.point.x, 0f, hit.point.z);
        }
        controller.navMeshAgent.SetDestination(targetWorld);
    }
}
