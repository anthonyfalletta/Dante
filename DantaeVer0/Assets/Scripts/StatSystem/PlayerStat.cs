using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public CharacterStat Speed;
    public CharacterStat DashSpeed;
    public CharacterStat DashDefaultSpeed;
    public CharacterStat DashDecrease;
    public CharacterStat DashDuration;
    public CharacterStat DashCooldown;
    public bool dashEnable;

    private void Start() {
       Speed.BaseValue = 100f;
       DashSpeed.BaseValue = 400f;
       DashDefaultSpeed.BaseValue = DashSpeed.Value;
       DashDecrease.BaseValue = 2.0f;
       DashDuration.BaseValue = 200f;
       DashCooldown.BaseValue = 1.0f;
       dashEnable = true;
       //ClassShoot.BaseValue = (float)ClassType.Default;

    }
}

//public enum ClassType{Default}
