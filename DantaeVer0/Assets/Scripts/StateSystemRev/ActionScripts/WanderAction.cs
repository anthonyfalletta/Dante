using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "StateSystem/Actions/Wander")]
public class WanderAction : StateAction
{
    public override void Act(StateController controller)
    {
        Wander(controller);
    }

    private void Wander(StateController controller){
        Debug.Log("Wandering.......");
    }
}
