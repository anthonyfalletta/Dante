using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    public PlayerDashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, PlayerStat statContext)
    :base(currentContext, playerStateFactory, statContext){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        Ctx.Animator.SetBool(Ctx.IsDashingHash, true);
        StartDash();
    }
    public override void UpdateState(){
        CheckSwitchStates();
        
    }

    public override void FixedUpdateState(){
        PerformDash();
    }
    public override void ExitState(){
        Ctx.Animator.SetBool(Ctx.IsDashingHash, false);
    }
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){

    }

    void StartDash()
    {
        Ctx.DashSpeed = 600f;
    }

    void PerformDash()
    {
        Vector2 dashDir = Ctx.MovementInputValue.normalized;
        Ctx.PlayerRb.velocity = dashDir * Stat.DashSpeed.Value * Time.deltaTime;
        DashSpeedSlowdown();
    }

    private void DashSpeedSlowdown()
    {
        Ctx.DashSpeed -= Ctx.DashSpeed * Stat.DashDecrease.Value * Time.deltaTime;

        if(Ctx.DashSpeed < Stat.DashDuration.Value)
        {
            SwitchState(Factory.Move());
        }
    }
}
