using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMoveState : PlayerBaseState
{
    public static Action moveInput;
    public static Action velocityZero;
    public static Action moveAnimatonActive;
    public static Action moveAnimatonDeactivate;

    public PlayerMoveState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base(currentContext, playerStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        moveAnimatonActive?.Invoke();
    }
    public override void UpdateState(){
        CheckSwitchStates();  
    }

    public override void FixedUpdateState(){
        moveInput?.Invoke();
    }
    public override void ExitState(){
        moveAnimatonDeactivate?.Invoke();
        velocityZero?.Invoke();
    }
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){
        if (Ctx.Input.IsDashPressed && Ctx.DashStateEnabled)
        {
            SwitchState(Factory.Dash());
        }
        if (Ctx.Input.IsShootPressed)
        {
            SwitchState(Factory.Shoot());
        }
        if (Ctx.Input.IsSpecialPressed)
        {
            SwitchState(Factory.Special());
        }
        else if (!Ctx.Input.IsMovePressed){
            SwitchState(Factory.Idle());
        }   
    }   
}
