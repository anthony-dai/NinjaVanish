using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/InvestigateDone")]
public class InvestigateDoneDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool done = InvestigateDone(controller);
        return done;
    }

    private bool InvestigateDone(StateController controller)
    {
        if (controller.lookAroundCount >= 4)
        {
            controller.lookAround = false;
            controller.lookAroundCount = 0;
            if (controller.m_CurrentWaypointIndex > 0) controller.m_CurrentWaypointIndex -= 1;
            else controller.m_CurrentWaypointIndex = controller.waypoints.Length - 1;
            return true;
        }
        else return false;
    }
}
