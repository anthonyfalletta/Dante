using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, PlayerStat statContext)
    :base(currentContext, playerStateFactory, statContext){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        StartIdle();
    }
    public override void UpdateState(){
        CheckSwitchStates();
        if (Keyboard.current.spaceKey.wasReleasedThisFrame && Stat.Speed.Value == 100f){Stat.Speed.BaseValue = 500f;}
        else if(Keyboard.current.spaceKey.wasReleasedThisFrame && Stat.Speed.Value == 500f){Stat.Speed.BaseValue = 100f;}
    }

    public override void FixedUpdateState(){}
    public override void ExitState(){}
    public override void InitializeSubState(){

    }
    public override void CheckSwitchStates(){
        if (Ctx.IsMovePressed)
        {
            SwitchState(Factory.Move());
        }
        else if (Ctx.IsShootPressed){
            SwitchState(Factory.Shoot());
        }
        else if (Ctx.IsAttackPressed){
            SwitchState(Factory.Attack());
        }
        else if (Ctx.IsSpecialPressed)
        {
            SwitchState(Factory.Special());
        }
    }

    void StartIdle()
    {
        
    }
}
