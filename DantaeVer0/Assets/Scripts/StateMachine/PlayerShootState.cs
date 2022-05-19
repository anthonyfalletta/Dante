using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootState : PlayerBaseState
{
    public PlayerShootState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base(currentContext, playerStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        StartShoot();
    }
    public override void UpdateState(){
        /*
        Ctx.RaycastPos =  new Vector2(Ctx.Rb.transform.position.x,Ctx.Rb.transform.position.y) + (Ctx.ShootingPos * Ctx.ShootingDistance);
        PerformShoot(); 
        */  
        CheckSwitchStates();
    }
    public override void ExitState(){
        
    }
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){
        if (!Ctx.IsShootPressed){
            SwitchState(Factory.Idle());
        }
    }

    void StartShoot()
    {
        Ctx.InstantiateProjectile();
    }

    //***Implement Fixed Update
    void PerformShoot()
    {
        /*
        //Debug.Log("Player enter SHOOT state");
        if (Ctx.ShootingDistance <= 2.0f)
        {
            Ctx.ShootingDistance += 1.25f * Time.deltaTime;
        }  
        
        RaycastingShootingPostion();
        ProjectileSphereMovement();
        */
    }

    private void ExitShoot()
    {
        Debug.Log("Either canceling or firing shot");
    }

    private void RaycastingShootingPostion()
    {
            if (Ctx.MovementInputValue != Vector2.zero)
            {
                Ctx.ShootingPos = Ctx.MovementInputValue.normalized;
            }
            //***Ctx.Rb.transform...
            Ctx.RaycastHit2D = Physics2D.Raycast(Ctx.Rb.transform.position, Ctx.ShootingPos,Ctx.ShootingDistance, Ctx.PlayerLayerMask);
            Debug.DrawRay(Ctx.Rb.transform.position, Ctx.ShootingPos*Ctx.ShootingDistance);

            if (Ctx.RaycastHit2D.collider == true)
            {
                Debug.Log("Contact");
                //shootingDistance = raycastHit2D.distance;                       
            }
            else 
            {
                Debug.Log("No Contact");
            }
    }

    private void ProjectileSphereMovement()
    {
        if(Ctx.MovementInputValue.normalized != Vector2.zero)
        {       
            //Debug.Log("Movement is zero");
            //Debug.Log("Vector 3 Normalized" + new Vector3(playerMovement.GetMovementValue().x,playerMovement.GetMovementValue().y,0).normalized);

            float angle = Mathf.Atan2(Ctx.MovementInputValue.normalized.y,Ctx.MovementInputValue.normalized.x) * Mathf.Rad2Deg;
            Ctx.ProjectileGO.transform.eulerAngles = new Vector3(0,0,angle);
            //projectile.transform.position = transform.position + (new Vector3(playerMovement.GetMovementValue().x,playerMovement.GetMovementValue().y,0).normalized * 0.1f);
            //***Ctx.Rb.transform
            Ctx.ProjectileGO.transform.position = Ctx.Rb.transform.position + (new Vector3(-Ctx.MovementInputValue.x,-Ctx.MovementInputValue.y,0).normalized * 0.1f);
        }   
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(Ctx.RaycastPos, 0.1f);
    }


}
