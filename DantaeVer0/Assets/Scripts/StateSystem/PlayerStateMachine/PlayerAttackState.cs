using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base(currentContext, playerStateFactory){
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState(){
        StartAttack();
    }
    public override void UpdateState(){ 
        CheckSwitchStates();
    }

    public override void FixedUpdateState(){
        PerformAttack();
    }
    public override void ExitState(){}
    public override void InitializeSubState(){}
    public override void CheckSwitchStates(){
        if (!Ctx.IsAttackPressed){
            SwitchState(Factory.Idle());
        }
    }

    void StartAttack()
    {

    }

    void PerformAttack()
    {
        float angle = Mathf.Atan2(Ctx.MovementInputValue.normalized.y,Ctx.MovementInputValue.normalized.x) * Mathf.Rad2Deg;
        Quaternion attackRotation = Quaternion.Euler(new Vector3(0,0,angle));
        Vector3 attackPosition = Ctx.PlayerGO.transform.position + (new Vector3(Ctx.MovementInputValue.x,Ctx.MovementInputValue.y,0).normalized * 0.1f);
        Ctx.AttackResetRoutine = Ctx.StartCoroutine((IAttackSwipe(attackPosition, attackRotation)));
    }

    IEnumerator IAttackSwipe(Vector3 position, Quaternion rotation)
    {
        Debug.Log("IAttackSwipe Script Running");
        Ctx.InstantiateAttackGameObject();
        yield return new WaitForSeconds(2f);
    }
}
