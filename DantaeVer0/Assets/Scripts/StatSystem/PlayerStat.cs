using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public CharacterStat Speed;
    public CharacterStat DashSpeed;
    public CharacterStat DashDecrease;
    public CharacterStat DashDuration;
    //public CharacterStat ClassShoot; 

    private void Start() {
       Speed.BaseValue = 100f;
       DashSpeed.BaseValue = 400f;
       DashDecrease.BaseValue = 4.0f;
       DashDuration.BaseValue = 150f;
       //ClassShoot.BaseValue = (float)ClassType.Default;

    }
}

//public enum ClassType{Default}
