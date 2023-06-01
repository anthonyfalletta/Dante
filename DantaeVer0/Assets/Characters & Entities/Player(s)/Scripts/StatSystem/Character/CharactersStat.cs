using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersStat : MonoBehaviour
{
    //General
    public GameObject Player;

    //Wander 
    public CharactersStatLogic Speed;
    public CharactersStatLogic MinWander;
    public CharactersStatLogic MaxWander;
    public  Vector3 wanderStartPoint;
    public  Vector3 wanderPreviousPoint;
    public  Vector3 wanderPoint;
    public  bool bWanderComplete;

    //Chase


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Speed.BaseValue = 1f;
        MinWander.BaseValue = 1.1f;
        MaxWander.BaseValue = 1.5f;

    }
}
