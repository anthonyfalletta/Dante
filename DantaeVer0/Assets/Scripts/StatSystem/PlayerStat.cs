using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public CharacterStat Speed;

    private void Start() {
       Speed.BaseValue = 100f;
    }
}
