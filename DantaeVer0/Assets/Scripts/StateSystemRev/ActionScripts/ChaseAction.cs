using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "StateSystem/Actions/Chase")]
public class ChaseAction : StateAction
{
    public override void Act(StateController controller)
    {
        
    }

    private void Chase(StateController controller)
    {
        Debug.Log("Chase Target");
    }
}
