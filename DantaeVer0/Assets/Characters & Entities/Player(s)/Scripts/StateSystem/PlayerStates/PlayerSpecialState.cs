using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialState : PlayerBaseState
{
    public PlayerSpecialState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
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
        if (!Ctx.Input.IsSpecialPressed)
        {
            SwitchState(Factory.Idle());
        }
        if (Ctx.Input.IsDashPressed)
        {
            SwitchState(Factory.Dash());
        }
    }
}
