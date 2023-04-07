using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        if (controller.IsDetected)
        {
            controller.lookAroundCount = 0;
            controller.lookAround = false;
            controller.navMeshAgent.speed = controller.enemyStats.sprintSpeed;
        }
        return controller.IsDetected;
    }
}
