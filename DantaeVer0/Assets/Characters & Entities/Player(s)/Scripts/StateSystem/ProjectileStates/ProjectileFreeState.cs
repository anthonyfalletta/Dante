using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFreeState : ProjectileBaseState
{
     public ProjectileFreeState(ProjectileStateMachine currentContext, ProjectileStateFactory projectileStateFactory)
    :base(currentContext, projectileStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        StartFree();
    }
    public override void UpdateState(){
        PerformFree();
        CheckSwitchStates();
    }

    public override void FixedUpdateState(){}
    public override void ExitState(){}
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){
        if (Ctx.IsSpecialPressed){
            SwitchState(Factory.Special());
        }
    }

    void StartFree()
    {
        Ctx.IsCollider = null;
    }
    void PerformFree()
    {
        if (Ctx.IsCollider != null)
        {
            Ctx.ProjectileDestroy();
        }
    }
}
