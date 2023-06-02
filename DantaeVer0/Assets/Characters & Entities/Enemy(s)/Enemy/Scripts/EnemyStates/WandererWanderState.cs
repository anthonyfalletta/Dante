using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WandererWanderState : EnemyBaseState
{
    public static Action wandering;
    public static Action wanderingCheck;
    public static Action seek;

    public WandererWanderState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
    :base(currentContext, enemyStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        Debug.Log("Is activating Enter State");
        //TODO Need to check if level has been loaded for some duration
        Ctx.Action.Wander();
        
    }

    public override void UpdateState(){
        CheckSwitchStates();
        Ctx.Action.Seek();
        Ctx.Action.CheckWanderComplete();
    }

    public override void FixedUpdateState(){}
    public override void ExitState(){}
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){}       
}
