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
        Ctx.Animator.EnableDashAnimation();
        DashReset();
    }
    public override void UpdateState(){
        CheckSwitchStates();
        
    }

    public override void FixedUpdateState(){
        Dash();
    }
    public override void ExitState(){
        Ctx.Animator.DisableDashAnimation();
    }
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){

    }

    public void Dash(){
        Ctx.DashStateEnabled = false;
        Ctx.Stat.DashSpeed.BaseValue = Ctx.Stat.DashDefaultSpeed.Value;
    }

    public void DashReset(){
        Vector2 dashDir = Ctx.Input.MovementInputValue.normalized;
        Ctx.PlayerRb.velocity = dashDir * Ctx.Stat.DashSpeed.Value * Time.deltaTime;
        Ctx.DashSpeedSlowdown();
    }
}
