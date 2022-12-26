using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "StateSystem/Decisions/ActiveState")]
public class ActiveStateDecision : StateDecision
{
    public override bool Decide(StateController state)
    {
        bool chaseTargetIsActive = true;
        return chaseTargetIsActive;
    }
}
