using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Hear")]
public class HearDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool isHeard = Hear(controller);
        return isHeard;
    }

    private bool Hear(StateController controller)
    {
        if (controller.IsHeard && !controller.IsDetected)
        {
            controller.IsHeard = false;
            controller.lookAround = false;
            controller.navMeshAgent.SetDestination(controller.susLocation);
            return true;
        }
        else return false;
    }
}
