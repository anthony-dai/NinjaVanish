using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Sweep")]
public class SweepDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool done = Sweep(controller);
        return done;
    }

    private bool Sweep(StateController controller)
    {
        return false;
        //if (controller.enemyStats.sweepArea)
        //{
        //    return true;
        //}
        //else return false;
    }
}
