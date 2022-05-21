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
        StartMovement();
    }
    public override void UpdateState(){
        HandleMoveAnimation();
        PerformMovement();
        CheckSwitchStates();  
    }
    public override void ExitState(){
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
        Ctx.PlayerRb.velocity = Ctx.MovementInputValue.normalized * 25f * Time.deltaTime;
    }

    public void HandleMoveAnimation()
    {
        

            Ctx.Animator.SetFloat("Horizontal", Ctx.MovementInputValue.x);
            Ctx.Animator.SetFloat("Vertical", Ctx.MovementInputValue.y);
            Ctx.Animator.SetFloat("Magnitude", Ctx.MovementInputValue.sqrMagnitude);
            Ctx.Animator.SetFloat("LastHorizontal", Ctx.LastMovementInputValue.x);
            Ctx.Animator.SetFloat("LastVertical", Ctx.LastMovementInputValue.y);       
    }
}
