using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpecialState : ProjectileBaseState
{
     public ProjectileSpecialState(ProjectileStateMachine currentContext, ProjectileStateFactory projectileStateFactory)
    :base(currentContext, projectileStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        StartSpecial();
    }
    public override void UpdateState(){
        CheckSwitchStates();
    }

    public override void FixedUpdateState(){
        PerformSpecial();
    }
    public override void ExitState(){}
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){
        if (!Ctx.IsSpecialPressed && Ctx.IsCollision == null){
            SwitchState(Factory.Free());
        }
    }

    void StartSpecial()
    {
        //*Wait for some time with Coroutine to start special (return process) 
        Ctx.SpecialWait = true;  
        Ctx.StartCoroutine(SpecialWaitToStart());    
    }

    void PerformSpecial()
    {
        if (Ctx.SpecialWait == false)
        {
            if (Ctx.IsCollision == null)
            {
                if (Ctx.IsCollider == null)
                {
                    Ctx.ProjectileGO.transform.position = Vector2.MoveTowards(Ctx.ProjectileGO.transform.position, Ctx.PlayerGO.transform.position, 0.5f*Time.deltaTime);      
                }
                else if (Ctx.IsCollider != null)
                {
                    Ctx.ProjectileDestroy();
                }
            }
        }
    }

    IEnumerator SpecialWaitToStart()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Ran this once?");
        Ctx.SpecialWait = false;
        
    }
}
