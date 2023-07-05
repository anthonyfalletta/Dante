using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttackState : PlayerBaseState
{
    public static Action attackInput;
    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base(currentContext, playerStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        
    }
    public override void UpdateState(){ 
        CheckSwitchStates();
    }

    public override void FixedUpdateState(){
        Attack();
    }
    public override void ExitState(){}
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){
        if (!Ctx.Input.IsAttackPressed){
            SwitchState(Factory.Idle());
        }
    }
    
    public void Attack()
    {
        float angle = Mathf.Atan2(Ctx.Input.MovementInputValue.normalized.y,Ctx.Input.MovementInputValue.normalized.x) * Mathf.Rad2Deg;
        Quaternion attackRotation = Quaternion.Euler(new Vector3(0,0,angle));
        Vector3 attackPosition = Ctx.Obj.PlayerGO.transform.position + (new Vector3(Ctx.Input.MovementInputValue.x,Ctx.Input.MovementInputValue.y,0).normalized * 0.1f);
        Ctx.AttackCo(attackPosition, attackRotation);
    }
}
