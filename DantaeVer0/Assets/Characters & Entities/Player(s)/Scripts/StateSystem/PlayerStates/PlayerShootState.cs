using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShootState : PlayerBaseState
{
    public static Action createProjectile;
    public PlayerShootState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base(currentContext, playerStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        ProjectileCreation();
    }
    public override void UpdateState(){
        CheckSwitchStates();
    }

    public override void FixedUpdateState(){}
    public override void ExitState(){
        
    }
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){
        if (!Ctx.Input.IsShootPressed){
            SwitchState(Factory.Idle());
        }
    }

    public void ProjectileCreation(){
        InstantiateProjectile();
    }
   
    public void InstantiateProjectile()
    {
        float angle = Mathf.Atan2(Ctx.Input.LastMovementInputValue.normalized.y,Ctx.Input.LastMovementInputValue.normalized.x) * Mathf.Rad2Deg;
        Quaternion playerRot = Quaternion.Euler(0,0,angle);
        Vector3 playerPos = Ctx.Obj.PlayerGO.transform.position + (new Vector3(-Ctx.Input.LastMovementInputValue.x,-Ctx.Input.LastMovementInputValue.y,0).normalized * 0.1f);
        Ctx.InstantiateProj(playerPos, playerRot);
    } 
}
