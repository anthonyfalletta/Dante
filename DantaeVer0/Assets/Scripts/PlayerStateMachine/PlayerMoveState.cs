using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base(currentContext, playerStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        Ctx.Animator.SetBool(Ctx.IsMovingHash, true);
        StartMovement();
    }
    public override void UpdateState(){
        //bool isWalking = Ctx.Animator.GetBool("isMoving");
        CheckSwitchStates();  
    }

    public override void FixedUpdateState(){
        PerformMovement();
    }
    public override void ExitState(){
        Ctx.Animator.SetBool(Ctx.IsMovingHash, false);
        Ctx.PlayerRb.velocity = Vector2.zero;
    }
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){
        if (Ctx.IsDashPressed)
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
        Ctx.PlayerRb.velocity = Ctx.MovementInputValue.normalized * 100f * Time.deltaTime;
    }

    
}
