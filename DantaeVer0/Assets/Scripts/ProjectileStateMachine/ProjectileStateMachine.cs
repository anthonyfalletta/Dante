using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileStateMachine : MonoBehaviour
{
    //State Variables
    ProjectileBaseState _currentState;
    ProjectileStateFactory _states;

    //Abilities Variables
    bool _isShootPressed;

    //Getters & Setters
    public ProjectileBaseState CurrentState{get{return _currentState;} set{_currentState = value;}}
    public bool IsShootPressed {get{return _isShootPressed;} set{_isShootPressed = value;}}

    private void Awake() 
    {
        //Setup State
        _states = new ProjectileStateFactory(this);
        _currentState = _states.Held();
        _currentState.EnterState();
      
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateStates();
        Debug.Log("Current State: " + _currentState);
    }

    public void OnShootButton(InputAction.CallbackContext context)
    {
        _isShootPressed = context.ReadValueAsButton();
        //Debug.Log("SHOOT Button Is Pressed");
    }
}
