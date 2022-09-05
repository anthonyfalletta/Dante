using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShootState : PlayerBaseState
{
    public static Action createProjectile;
    public PlayerShootState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base(currentContext, playerStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        createProjectile?.Invoke();
    }
    public override void UpdateState(){
        CheckSwitchStates();
    }

    public override void FixedUpdateState(){}
    public override void ExitState(){
        
    }
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){
        if (!Ctx.Input.IsShootPressed){
            SwitchState(Factory.Idle());
        }
    }
}
