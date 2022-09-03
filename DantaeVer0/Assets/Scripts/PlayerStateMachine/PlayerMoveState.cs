using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMoveState : PlayerBaseState
{
    public static Action moveInput;
    public static Action velocityZero;
    public PlayerMoveState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, PlayerStat statContext)
    :base(currentContext, playerStateFactory, statContext){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        Ctx.Animator.SetBool(Ctx.IsMovingHash, true);
        StartMovement();
    }
    public override void UpdateState(){
        CheckSwitchStates();  
    }

    public override void FixedUpdateState(){
        PerformMovement();
    }
    public override void ExitState(){
        Ctx.Animator.SetBool(Ctx.IsMovingHash, false);
        velocityZero?.Invoke();
    }
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){
        if (Ctx.IsDashPressed && Stat.dashEnable)
        {
            SwitchState(Factory.Dash());
        }
        if (Ctx.IsShootPressed)
        {
            SwitchState(Factory.Shoot());
        }
        if (Ctx.IsSpecialPressed)
        {
            SwitchState(Factory.Special());
        }
        else if (!Ctx.IsMovePressed){
            SwitchState(Factory.Idle());
        }   
    }

    void StartMovement()
    {

    }
    void PerformMovement()
    {
        moveInput?.Invoke();
    }

    
}
