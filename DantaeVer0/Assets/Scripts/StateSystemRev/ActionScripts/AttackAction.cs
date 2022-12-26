using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "StateSystem/Actions/Attack")]
public class AttackAction : StateAction
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        Debug.Log("Attack........");
    }
}
