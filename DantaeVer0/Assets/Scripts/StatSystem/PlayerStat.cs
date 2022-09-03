using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public CharacterStat Speed;
    public CharacterStat DashSpeed;
    public CharacterStat DashDecrease;
    public CharacterStat DashDuration;
    public CharacterStat DashCooldown;
    public bool dashEnable;

    private void Start() {
       Speed.BaseValue = 100f;
       DashSpeed.BaseValue = 200f;
       DashDecrease.BaseValue = 1.0f;
       DashDuration.BaseValue = 150f;
       dashEnable = true;
       //ClassShoot.BaseValue = (float)ClassType.Default;

    }
}

//public enum ClassType{Default}
