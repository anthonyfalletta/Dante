using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHeldState : ProjectileBaseState
{
    public ProjectileHeldState(ProjectileStateMachine currentContext, ProjectileStateFactory projectileStateFactory)
    :base(currentContext, projectileStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        StartHeld();
    }
    public override void UpdateState(){
        CheckSwitchStates();
    }
    public override void ExitState(){
   
    }
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){
        if (Ctx.IsShootPressed){
           
            SwitchState(Factory.Charge());
        }
        else
        {
            Ctx.ProjectileDestroy();
        }

    }

    void StartHeld()
    {

    }
}
