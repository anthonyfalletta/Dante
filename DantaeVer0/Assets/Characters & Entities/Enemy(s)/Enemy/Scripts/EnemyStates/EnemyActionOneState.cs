using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionOneState : EnemyBaseState
{
    public EnemyActionOneState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
    :base(currentContext, enemyStateFactory){
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
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){}
}
