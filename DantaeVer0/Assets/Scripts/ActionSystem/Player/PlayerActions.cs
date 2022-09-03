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
    }

    void Update()
    {
        
    }


    void DashReset(){
        Stat.DashSpeed.BaseValue = 600f;
    }

    

    void Dash(){
        Vector2 dashDir = Ctx.MovementInputValue.normalized;
        Ctx.PlayerRb.velocity = dashDir * Stat.DashSpeed.Value * Time.deltaTime;
        //DashSpeedSlowdown();
       StartCoroutine(DashSpeedSlowdownCo());
        
    }

    private void DashSpeedSlowdown()
    {
        Stat.DashSpeed.BaseValue -= Stat.DashSpeed.Value * Stat.DashDecrease.Value * Time.deltaTime;

        if(Stat.DashSpeed.Value < Stat.DashDuration.Value)
        {
            DashReset();
            Ctx.CurrentState.SwitchState(Ctx.States.Move());
        }
    }

    IEnumerator DashSpeedSlowdownCo()
    {
        while(Stat.DashSpeed.Value > Stat.DashDuration.Value)
        {
            //Vector2 dashDir = Ctx.MovementInputValue.normalized;
            //Ctx.PlayerRb.velocity = dashDir * Stat.DashSpeed.Value * Time.deltaTime;
            Stat.DashSpeed.BaseValue -= Stat.DashSpeed.Value * Stat.DashDecrease.Value * Time.deltaTime;
            yield break;
            
        }
        //DashReset();
        Ctx.CurrentState.SwitchState(Ctx.States.Move());
        
    }
}
