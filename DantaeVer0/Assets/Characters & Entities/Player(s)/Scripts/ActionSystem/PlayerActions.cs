using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    

    public bool isMoving = false;
    public bool isZero = false;
    public bool isDashing = false;

    public Vector2 inputValue;
    public Vector2 inputValueNormalized;

    private void Awake() {
        
    }
    
    void Start()
    {

    }

    void Update()
    {
        //inputValue = Input.MovementInputValue;
        //inputValueNormalized = Input.MovementInputValue.normalized;
        //SetVelocityZero();
        //MoveLogic();
    }

    /*

    void FixedUpdate(){
        
        
    }

    public void SetVelocityZero(){
        isZero=true;
    }

    public void SetVelocityZeroLogic(){
        isMoving = false;
        Obj.PlayerRb.velocity = Vector2.zero;
        isZero = false;
    }

    public void Move(){
        isMoving = true;
    }

    public void MoveLogic()
    {
        if (isMoving){
            Obj.PlayerRb.velocity = Input.MovementInputValue.normalized  * Stat.Speed.Value * 0.5f * Time.deltaTime;
        }
    }

    public void DashLogic(){

    }

    public void DashReset(){
        Ctx.DashStateEnabled = false;
        Stat.DashSpeed.BaseValue = Stat.DashDefaultSpeed.Value;
    }

    public void Dash(){
        Vector2 dashDir = Input.MovementInputValue.normalized;
        Obj.PlayerRb.velocity = dashDir * Stat.DashSpeed.Value * Time.deltaTime;
        StartCoroutine(IDashSpeedSlowdown());
        
    }

    IEnumerator IDashSpeedSlowdown()
    {
        while(Stat.DashSpeed.Value > Stat.DashDuration.Value)
        {
            Stat.DashSpeed.BaseValue -= Stat.DashSpeed.Value * Stat.DashDecrease.Value * Time.deltaTime;
            yield break;
        }
        StartCoroutine(ICooldownTimer(Stat.DashCooldown.Value));
        Ctx.CurrentState.SwitchState(Ctx.States.Move());    
    }

    IEnumerator ICooldownTimer(float dashCooldownTime)
    {
            yield return new WaitForSeconds(dashCooldownTime);
            Ctx.DashStateEnabled = true;
    }

    public void Attack()
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

    public void InstantiateAttackGameObject()
    {
        float angle = Mathf.Atan2(Input.LastMovementInputValue.normalized.y,Input.LastMovementInputValue.normalized.x) * Mathf.Rad2Deg;
        Quaternion playerRot = Quaternion.Euler(0,0,angle);
        Vector3 playerPos = Obj.PlayerGO.transform.position + (new Vector3(Input.LastMovementInputValue.x,Input.LastMovementInputValue.y,0).normalized * 0.1f);
        Instantiate(Obj.AttackObject, playerPos, playerRot);
    }

    public void ProjectileCreation(){
        InstantiateProjectile();
    }
   
    public void InstantiateProjectile()
    {
        float angle = Mathf.Atan2(Input.LastMovementInputValue.normalized.y,Input.LastMovementInputValue.normalized.x) * Mathf.Rad2Deg;
        Quaternion playerRot = Quaternion.Euler(0,0,angle);
        Vector3 playerPos = Obj.PlayerGO.transform.position + (new Vector3(-Input.LastMovementInputValue.x,-Input.LastMovementInputValue.y,0).normalized * 0.1f);
        Instantiate(Obj.ProjectilePrefab, playerPos, playerRot);
    } 
    */
}
