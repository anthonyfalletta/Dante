using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMovementOneState : EnemyBaseState
{
    public static Action follow;

    public EnemyMovementOneState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
    :base(currentContext, enemyStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        follow?.Invoke();
    }
    public override void UpdateState(){
        CheckSwitchStates();
        follow?.Invoke();
    }

    public override void FixedUpdateState(){}
    public override void ExitState(){}
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){}
}
