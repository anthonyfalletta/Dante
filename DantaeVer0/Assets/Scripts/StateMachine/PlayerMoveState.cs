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
        CheckSwitchStates();
        PerformMovement();
    }
    public override void ExitState(){
        Debug.Log("Exit MOVE State");
    }
    public override void InitializeSubState(){
        if (Ctx.IsDashPressed)
        {
            SetSubState(Factory.Dash());
        }

    }
    public override void CheckSwitchStates(){
        if (Ctx.IsDashPressed)
        {
            SwitchState(Factory.Dash());
        }
        else if (!Ctx.IsMovePressed){
            SwitchState(Factory.Idle());
        }   
    }

    void StartMovement()
    {
        //Debug.Log("Player enters MOVE state");
    }
    void PerformMovement()
    {
        //Debug.Log("Performing MOVE state");
        Ctx.Rb.velocity = Ctx.MovementInputValue.normalized * 25f * Time.deltaTime;
        //playerAbilitiesAnimations.HandleMoveAnimation(movementValue.normalized);
    }
}
