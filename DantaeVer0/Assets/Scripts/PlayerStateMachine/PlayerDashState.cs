using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDashState : PlayerBaseState
{
    public static Action dashInput;
    public static Action dashReset;

    public PlayerDashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, PlayerStat statContext)
    :base(currentContext, playerStateFactory, statContext){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        Ctx.Animator.SetBool(Ctx.IsDashingHash, true);
        StartDash();
    }
    public override void UpdateState(){
        CheckSwitchStates();
        
    }

    public override void FixedUpdateState(){
        PerformDash();
    }
    public override void ExitState(){
        Ctx.Animator.SetBool(Ctx.IsDashingHash, false);
    }
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){

    }

    void StartDash()
    {
        dashReset?.Invoke();
    }

    void PerformDash()
    {
        dashInput?.Invoke();
    }
}
