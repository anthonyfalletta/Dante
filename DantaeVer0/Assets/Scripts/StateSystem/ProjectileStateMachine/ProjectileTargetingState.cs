using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTargetingState : ProjectileBaseState
{
     public ProjectileTargetingState(ProjectileStateMachine currentContext, ProjectileStateFactory projectileStateFactory)
    :base(currentContext, projectileStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        StartTargeting();
    }
    public override void UpdateState(){
        CheckSwitchStates();
    }

    public override void FixedUpdateState(){
        PerformTargeting();
    }
    public override void ExitState(){}
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){

    }

    void StartTargeting()
    {
        
    }

    void PerformTargeting()
    {
        if (Ctx.IsCollision == null)
       {
        Ctx.ProjectileGO.transform.position = Vector2.MoveTowards(Ctx.ProjectileGO.transform.position, Ctx.TargetPos, 2.0f*Time.deltaTime);
        //*Debug.Log("TargetPosition: " + Ctx.TargetPos);
        //*Debug.Log("Magnitude: " + (Ctx.ProjectileGO.transform.position + Ctx.TargetPos).magnitude);

        //***Not Finding Proper Distance Between May Be Due to Precision or Something Else
        
        SpecialActivation();

       }  
       else
       {
           Debug.Log("Collision occur with Player Projectile");
           //***Will need to activate logic for collisions with other gameobjects
       }
    }

    void SpecialActivation()
    {
        if (Vector2.Distance(Ctx.ProjectileGO.transform.position,Ctx.TargetPos) == 0)
        {
            SwitchState(Factory.Free());
        }
    }
}
