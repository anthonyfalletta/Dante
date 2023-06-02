using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttackState : PlayerBaseState
{
    public static Action attackInput;
    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base(currentContext, playerStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        
    }
    public override void UpdateState(){ 
        CheckSwitchStates();
    }

    public override void FixedUpdateState(){
        Ctx.Action.Attack();
    }
    public override void ExitState(){}
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){
        if (!Ctx.Action.Input.IsAttackPressed){
            SwitchState(Factory.Idle());
        }
    }
}
