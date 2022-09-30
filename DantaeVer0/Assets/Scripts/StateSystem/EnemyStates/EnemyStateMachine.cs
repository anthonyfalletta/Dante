using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    //State Variables
    EnemyBaseState _currentState;
    EnemyStateFactory _states;

    Unit _unit;


    //Getters & Setters
    public EnemyBaseState CurrentState{get{return _currentState;} set{_currentState = value;}}
    public EnemyStateFactory States{get{return _states;} set{_states = value;}}

    public Unit EnemyPahtfinding{get{return _unit;} set{_unit = value;}}

    private void Awake() 
    {
        //Setup State
        _states = new EnemyStateFactory(this);
        _currentState = _states.MovementZero();
        _currentState.EnterState();

        _unit = this.GetComponent<Unit>(); 
    }
    
    void Start()
    {
        
    }

    
    void Update()
    {
        _currentState.UpdateStates();
        Debug.Log("Current State: " + CurrentState); 
    }

    void FixedUpdate() {
        _currentState.FixedUpdateStates();
    }

    public void Pathing(){
        StartCoroutine(DelayPathfinding());
    }

    IEnumerator DelayPathfinding(){
        yield return new WaitForSeconds(5);
        EnemyPahtfinding.ToggleUnitFollow();
    }


}
