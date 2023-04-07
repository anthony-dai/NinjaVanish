using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        controller.navMeshAgent.speed = controller.enemyStats.walkSpeed;
        controller.navMeshAgent.SetDestination(controller.waypoints[controller.m_CurrentWaypointIndex].position);
        if (controller.navMeshAgent.remainingDistance < controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        {
            controller.m_CurrentWaypointIndex = (controller.m_CurrentWaypointIndex + 1) % controller.waypoints.Length;
        }
    }
}
