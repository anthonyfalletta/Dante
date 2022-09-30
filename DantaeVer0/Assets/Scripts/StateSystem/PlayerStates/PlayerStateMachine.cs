using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerStateMachine : MonoBehaviour
{
    //State Variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    PlayerInputController _input;

    bool _dashStateEnabled;

    //Getters & Setters
    public PlayerBaseState CurrentState{get{return _currentState;} set{_currentState = value;}}
    public PlayerStateFactory States{get{return _states;} set{_states = value;}}

    public PlayerInputController Input {get{return _input;} set{_input = value;}}

    public bool DashStateEnabled {get{return _dashStateEnabled;} set{_dashStateEnabled = value;}}

    private void Awake() 
    {
        //Setup State
        _states = new PlayerStateFactory(this);
        _currentState = _states.Idle();
        _currentState.EnterState();

        _input = this.GetComponent<PlayerInputController>(); 
    }
    
    void Start()
    {
        _dashStateEnabled = true;
    }

    void Update()
    {
        _currentState.UpdateStates();
        Debug.Log("Current State: " + CurrentState);   
    }

    void FixedUpdate() {
        _currentState.FixedUpdateStates();
    }
}
