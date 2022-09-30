using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base(currentContext, playerStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
    }
    public override void UpdateState(){
        CheckSwitchStates();
    }

    public override void FixedUpdateState(){}
    public override void ExitState(){}
    public override void InitializeSubState(){

    }
    public override void CheckSwitchStates(){
        if (Ctx.Input.IsMovePressed)
        {
            SwitchState(Factory.Move());
        }
        else if (Ctx.Input.IsShootPressed){
            SwitchState(Factory.Shoot());
        }
        else if (Ctx.Input.IsAttackPressed){
            SwitchState(Factory.Attack());
        }
        else if (Ctx.Input.IsSpecialPressed)
        {
            SwitchState(Factory.Special());
        }
    }
}
