using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttackState : PlayerBaseState
{
    public static Action attackInput;
    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, PlayerStat statContext)
    :base(currentContext, playerStateFactory, statContext){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        StartAttack();
    }
    public override void UpdateState(){ 
        CheckSwitchStates();
    }

    public override void FixedUpdateState(){
        PerformAttack();
    }
    public override void ExitState(){}
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){
        if (!Ctx.IsAttackPressed){
            SwitchState(Factory.Idle());
        }
    }

    void StartAttack()
    {

    }

    void PerformAttack()
    {
        attackInput?.Invoke();
    }
}
