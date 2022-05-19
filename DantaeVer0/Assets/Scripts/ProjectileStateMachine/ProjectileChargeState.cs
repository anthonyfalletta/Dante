﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileChargeState : ProjectileBaseState
{
    public ProjectileChargeState(ProjectileStateMachine currentContext, ProjectileStateFactory projectileStateFactory)
    :base(currentContext, projectileStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        StartCharge();
    }
    public override void UpdateState(){
        CheckSwitchStates();
    }
    public override void ExitState(){}
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){
        /*
        if (Ctx.IsMovePressed)
        {
            SwitchState(Factory.Move());
        }
        else if (Ctx.IsShootPressed){
            SwitchState(Factory.Shoot());
        }
        else if (Ctx.IsAttackPressed){
            SwitchState(Factory.Attack());
        }
        */
    }

    void StartCharge()
    {
        
    }
}
