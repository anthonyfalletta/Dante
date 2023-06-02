using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    //State Variables
    EnemyBaseState _currentState;
    EnemyStateFactory _states;
    EnemyActions _actions;

    


    //Getters & Setters
    public EnemyBaseState CurrentState{get{return _currentState;} set{_currentState = value;}}
    public EnemyStateFactory States{get{return _states;} set{_states = value;}}
    public EnemyActions Action{get{return _actions;} set{_actions=value;}}
    

    private void Awake() 
    {
        //Setup State
        _states = new EnemyStateFactory(this);
    }
    
    void Start()
    {
        _actions = this.GetComponent<EnemyActions>();
    }

    public void StartActivation(){
        _currentState = _states.Wander();
        _currentState.EnterState();
    }
    
    void Update()
    {
        _currentState.UpdateStates();
        //Debug.Log("Current State: " + CurrentState); 
    }

    void FixedUpdate() {
        _currentState.FixedUpdateStates();
    }



}
