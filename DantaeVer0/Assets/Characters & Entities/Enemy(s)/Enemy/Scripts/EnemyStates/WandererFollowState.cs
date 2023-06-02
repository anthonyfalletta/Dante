using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WandererFollowState : EnemyBaseState
{
    public static Action follow;

    public WandererFollowState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
    :base(currentContext, enemyStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        Ctx.Action.Follow();
    }
    public override void UpdateState(){
        CheckSwitchStates();
        Ctx.Action.Follow();
    }

    public override void FixedUpdateState(){}
    public override void ExitState(){}
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){}
}
