using System.Collections;
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

    public override void FixedUpdateState(){
        PerformCharge();
    }
    public override void ExitState(){}
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){

     
       if (!Ctx.IsShootPressed){
           if (Vector3.Distance(Ctx.PlayerGO.transform.position, Ctx.RaycastPos) > 1.0f)
            {        
                Ctx.TargetPos = Ctx.RaycastPos;
                SwitchState(Factory.Target());
            } 
            else
            {
                Ctx.ProjectileDestroy();
            }      
        } 
    }

    void StartCharge()
    {
        Ctx.ShootingPos = Vector2.zero;
        Ctx.ShootingDistance = 0;
        Ctx.RaycastPos = Ctx.PlayerGO.transform.position;
    }

    void PerformCharge()
    {
        if (Ctx.ShootingDistance <= 8.0f)
        {
            Ctx.ShootingDistance += 2.0f * Time.deltaTime;
        }  
        Ctx.RaycastPos =  new Vector2(Ctx.PlayerGO.transform.position.x,Ctx.PlayerGO.transform.position.y) + (Ctx.ShootingPos * Ctx.ShootingDistance);

        RaycastingShootingPostion();
        ProjectileSphereMovement();
    }

    private void RaycastingShootingPostion()
    {
            if (Ctx.MovementInputValue != Vector2.zero)
            {
                Ctx.ShootingPos = Ctx.MovementInputValue.normalized;
            }
            Ctx.RaycastHit2D = Physics2D.Raycast(Ctx.PlayerGO.transform.position, Ctx.ShootingPos,Ctx.ShootingDistance, Ctx.PlayerLayerMask);
            Debug.DrawRay(Ctx.PlayerGO.transform.position, Ctx.ShootingPos*Ctx.ShootingDistance);

            if (Ctx.RaycastHit2D.collider == true)
            {
                //Debug.Log("Contact");
                //***Used to only raycast up to target
                //*shootingDistance = raycastHit2D.distance;                       
            }
            else 
            {
                //Debug.Log("No Contact");
            }
    }

    private void ProjectileSphereMovement()
    {
        if(Ctx.MovementInputValue.normalized != Vector2.zero)
        {       
            float angle = Mathf.Atan2(Ctx.MovementInputValue.normalized.y,Ctx.MovementInputValue.normalized.x) * Mathf.Rad2Deg;
            Ctx.ProjectileGO.transform.eulerAngles = new Vector3(0,0,angle);
            Ctx.ProjectileGO.transform.position = Ctx.PlayerGO.transform.position + (new Vector3(-Ctx.MovementInputValue.x,-Ctx.MovementInputValue.y,0).normalized * 0.1f);
        }   
    }
}
