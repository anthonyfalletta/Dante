using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "StateSystem/Actions/Chase")]
public class ChaseAction : StateAction
{
    public override void ActStart(StateController controller)
    {
        
    }

    public override void ActUpdate(StateController controller)
    {
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        controller.pathfinding.SetTarget(controller.data.Player.transform.position, controller.data.Speed.Value);
    }
}
