using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    PlayerStateMachine Ctx;
    PlayerStat Stat;


    void Start()
    {
        Ctx = this.GetComponent<PlayerStateMachine>();
        Stat = this.GetComponent<PlayerStat>();
        PlayerDashState.dashInput += Dash;
        PlayerDashState.dashReset += DashReset;
        PlayerMoveState.moveInput += Move;
        PlayerMoveState.velocityZero += SetVelocityZero;
        PlayerAttackState.attackInput += Attack;
        PlayerShootState.createProjectile += ProjectileCreation;
    }

    void Update()
    {
        
    }

    IEnumerator CooldownTimer(float dashCooldownTime)
    {
            yield return new WaitForSeconds(dashCooldownTime);
            Stat.dashEnable = true;
    }

    void DashReset(){
        Stat.dashEnable = false;
        Stat.DashSpeed.BaseValue = Stat.DashDefaultSpeed.Value;
    }

    

    void Dash(){
        Vector2 dashDir = Ctx.MovementInputValue.normalized;
        Ctx.PlayerRb.velocity = dashDir * Stat.DashSpeed.Value * Time.deltaTime;
        StartCoroutine(DashSpeedSlowdown());
        
    }

    IEnumerator DashSpeedSlowdown()
    {
        while(Stat.DashSpeed.Value > Stat.DashDuration.Value)
        {
            Stat.DashSpeed.BaseValue -= Stat.DashSpeed.Value * Stat.DashDecrease.Value * Time.deltaTime;
            yield break;
        }
        StartCoroutine(CooldownTimer(Stat.DashCooldown.Value));
        Ctx.CurrentState.SwitchState(Ctx.States.Move());    
    }

    void SetVelocityZero(){
        Ctx.PlayerRb.velocity = Vector2.zero;
    }

    void Move(){
        Ctx.PlayerRb.velocity = Ctx.MovementInputValue.normalized * Stat.Speed.Value * Time.deltaTime;
    }

    void Attack()
    {
        float angle = Mathf.Atan2(Ctx.MovementInputValue.normalized.y,Ctx.MovementInputValue.normalized.x) * Mathf.Rad2Deg;
        Quaternion attackRotation = Quaternion.Euler(new Vector3(0,0,angle));
        Vector3 attackPosition = Ctx.PlayerGO.transform.position + (new Vector3(Ctx.MovementInputValue.x,Ctx.MovementInputValue.y,0).normalized * 0.1f);
        Ctx.StartCoroutine((IAttackSwipe(attackPosition, attackRotation)));
    }

    IEnumerator IAttackSwipe(Vector3 position, Quaternion rotation)
    {
        Ctx.InstantiateAttackGameObject();
        yield return new WaitForSeconds(2f);
    }

    void ProjectileCreation(){
        Ctx.InstantiateProjectile();
    }

    
    
}
