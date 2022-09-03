using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShootState : PlayerBaseState
{
    public static Action createProjectile;
    public PlayerShootState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, PlayerStat statContext)
    :base(currentContext, playerStateFactory, statContext){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        StartShoot();
    }
    public override void UpdateState(){
        CheckSwitchStates();
    }

    public override void FixedUpdateState(){}
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
        //Ctx.InstantiateProjectile();
        createProjectile?.Invoke();
    }

    void PerformShoot()
    {

    }

    private void ExitShoot()
    {
   
    }
}
