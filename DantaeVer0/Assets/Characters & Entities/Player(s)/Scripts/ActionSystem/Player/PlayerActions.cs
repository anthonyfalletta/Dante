using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    PlayerStateMachine _ctx;
    PlayerStat _stat;
    PlayerInputController _input;
    PlayerObject _obj;

    public PlayerStateMachine Ctx {get{return _ctx;} set{_ctx = value;}}
    public PlayerStat Stat {get{return _stat;} set{_stat = value;}}
    public PlayerInputController Input {get{return _input;} set{_input = value;}}
    public PlayerObject Obj {get{return _obj;} set{_obj = value;}}

    private void Awake() {
        _ctx = this.GetComponent<PlayerStateMachine>();
        _stat = this.GetComponent<PlayerStat>();
        _input = this.GetComponent<PlayerInputController>();
        _obj = this.GetComponent<PlayerObject>();
    }
    void Start()
    {
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

    void SetVelocityZero(){
        Obj.PlayerRb.velocity = Vector2.zero;
    }

    void Move(){
        Obj.PlayerRb.velocity = Input.MovementInputValue.normalized * Stat.Speed.Value * Time.deltaTime;
    }

    void DashReset(){
        Ctx.DashStateEnabled = false;
        Stat.DashSpeed.BaseValue = Stat.DashDefaultSpeed.Value;
    }

    void Dash(){
        Vector2 dashDir = Input.MovementInputValue.normalized;
        Obj.PlayerRb.velocity = dashDir * Stat.DashSpeed.Value * Time.deltaTime;
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

    IEnumerator CooldownTimer(float dashCooldownTime)
    {
            yield return new WaitForSeconds(dashCooldownTime);
            Ctx.DashStateEnabled = true;
    }

    void Attack()
    {
        float angle = Mathf.Atan2(Input.MovementInputValue.normalized.y,Input.MovementInputValue.normalized.x) * Mathf.Rad2Deg;
        Quaternion attackRotation = Quaternion.Euler(new Vector3(0,0,angle));
        Vector3 attackPosition = Obj.PlayerGO.transform.position + (new Vector3(Input.MovementInputValue.x,Input.MovementInputValue.y,0).normalized * 0.1f);
        Ctx.StartCoroutine((IAttackSwipe(attackPosition, attackRotation)));
    }

    IEnumerator IAttackSwipe(Vector3 position, Quaternion rotation)
    {
        InstantiateAttackGameObject();
        yield return new WaitForSeconds(2f);
    }

    void InstantiateAttackGameObject()
    {
        float angle = Mathf.Atan2(Input.LastMovementInputValue.normalized.y,Input.LastMovementInputValue.normalized.x) * Mathf.Rad2Deg;
        Quaternion playerRot = Quaternion.Euler(0,0,angle);
        Vector3 playerPos = Obj.PlayerGO.transform.position + (new Vector3(Input.LastMovementInputValue.x,Input.LastMovementInputValue.y,0).normalized * 0.1f);
        Instantiate(Obj.AttackObject, playerPos, playerRot);
    }

    void ProjectileCreation(){
        InstantiateProjectile();
    }
   
    void InstantiateProjectile()
    {
        float angle = Mathf.Atan2(Input.LastMovementInputValue.normalized.y,Input.LastMovementInputValue.normalized.x) * Mathf.Rad2Deg;
        Quaternion playerRot = Quaternion.Euler(0,0,angle);
        Vector3 playerPos = Obj.PlayerGO.transform.position + (new Vector3(-Input.LastMovementInputValue.x,-Input.LastMovementInputValue.y,0).normalized * 0.1f);
        Instantiate(Obj.ProjectilePrefab, playerPos, playerRot);
    } 
}
