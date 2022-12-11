using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public EnemiesStat Speed;
    public EnemiesStat MinWander;
    public EnemiesStat MaxWander;

    // Start is called before the first frame update
    void Start()
    {
        Speed.BaseValue = 1f;
        MinWander.BaseValue = 1.1f;
        MaxWander.BaseValue = 1.5f;

    }

}
