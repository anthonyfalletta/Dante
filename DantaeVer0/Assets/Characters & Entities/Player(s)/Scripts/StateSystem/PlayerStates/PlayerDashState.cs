using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDashState : PlayerBaseState
{
    public static Action dashInput;
    public static Action dashReset;
    public static Action dashAnimatonActive;
    public static Action dashAnimatonDeactive;

    public PlayerDashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base(currentContext, playerStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        Ctx.Action.Animator.EnableDashAnimation();
        Ctx.Action.DashReset();
    }
    public override void UpdateState(){
        CheckSwitchStates();
        
    }

    public override void FixedUpdateState(){
        Ctx.Action.Dash();
    }
    public override void ExitState(){
        Ctx.Action.Animator.DisableDashAnimation();
    }
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){

    }
}
