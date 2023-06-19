using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMoveState : PlayerBaseState
{
    //public static Action moveInput;
    public static Action velocityZero;
    public static Action moveAnimatonActive;
    public static Action moveAnimatonDeactivate;

    public PlayerMoveState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base(currentContext, playerStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        Ctx.Action.Animator.EnableMoveAnimation();
        Ctx.Action.Move();
    }
    public override void UpdateState(){
        CheckSwitchStates();  
    }

    public override void FixedUpdateState(){
        
    }
    public override void ExitState(){
        Ctx.Action.Animator.DisableMoveAnimation();
        Ctx.Action.SetVelocityZero();
    }
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){
        if (Ctx.Action.Input.IsDashPressed && Ctx.DashStateEnabled)
        {
            SwitchState(Factory.Dash());
        }
        if (Ctx.Action.Input.IsShootPressed)
        {
            SwitchState(Factory.Shoot());
        }
        if (Ctx.Action.Input.IsSpecialPressed)
        {
            SwitchState(Factory.Special());
        }
        else if (!Ctx.Action.Input.IsMovePressed){
            SwitchState(Factory.Idle());
        }   
    }   
}
