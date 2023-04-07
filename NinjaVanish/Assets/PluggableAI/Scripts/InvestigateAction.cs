using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Investigate")]
public class InvestigateAction : Action
{
    public override void Act(StateController controller)
    {
        Investigate(controller);
    }

    private void Investigate(StateController controller)
    {
        if (controller.lookAround)
        {
            Vector3 desiredForward = controller.transform.forward;
            if (controller.lookAroundCount == 0 || controller.lookAroundCount == 2)
            {
                desiredForward = Vector3.RotateTowards(controller.transform.forward, controller.leftBound, controller.enemyStats.turnSpeed * Time.deltaTime, 0f);
                if (Vector3.Angle(controller.transform.forward, controller.leftBound) < 1.0f) controller.lookAroundCount += 1;
            }
            else if (controller.lookAroundCount == 1 || controller.lookAroundCount == 3)
            {
                desiredForward = Vector3.RotateTowards(controller.transform.forward, controller.rightBound, controller.enemyStats.turnSpeed * Time.deltaTime, 0f);
                if (Vector3.Angle(controller.transform.forward, controller.rightBound) < 1.0f) controller.lookAroundCount += 1;
            }

            Quaternion enemyRotation = Quaternion.LookRotation(desiredForward);
            controller.rb.MoveRotation(enemyRotation);
        }
        else if ((controller.navMeshAgent.remainingDistance < controller.navMeshAgent.stoppingDistance) && !controller.lookAround)
        {
            // If transitioned from Chasing to Suspicious then change the navmeshagent speed to walking again
            controller.navMeshAgent.speed = controller.enemyStats.walkSpeed;
            controller.rightBound = new Vector3(-controller.transform.forward.z, 0f, controller.transform.forward.x).normalized;
            controller.leftBound = new Vector3(controller.transform.forward.z, 0f, -controller.transform.forward.x).normalized;
            controller.lookAround = true;
            controller.lookAroundCount = 0;
        }
    }
}
