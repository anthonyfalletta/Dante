using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "StateSystem/Decisions/Seek")]
public class SeekDecision : StateDecision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Seek(controller);
        return targetVisible;
    }

    private bool Seek(StateController controller){
        if (Vector2.Distance(controller.data.Player.transform.position,controller.gameObject.transform.position) < 4.0f)
        {
            return true;
        }
        return false;

    }
}
