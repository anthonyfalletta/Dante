using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    //State Variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    //Abilities Variables
    bool _isMovePressed;
    bool _isDashPressed;
    bool _isShootPressed;
    bool _isAttackPressed;
    
    //Player Variable
    Rigidbody2D _rb;
    Vector2 _movementInputValue;
    //Movement Variables
    float _dashSpeed;


    //Getters & Setters
    public PlayerBaseState CurrentState{get{return _currentState;} set{_currentState = value;}}
    public bool IsMovePressed {get{return _isMovePressed;} set{_isMovePressed = value;}}
    public bool IsDashPressed {get{return _isDashPressed;} set{_isDashPressed = value;}}
    public bool IsShootPressed {get{return _isShootPressed;} set{_isShootPressed = value;}}
    public bool IsAttackPressed {get{return _isAttackPressed;} set{_isAttackPressed = value;}}

    public Rigidbody2D Rb {get{return _rb;}}

    public Vector2 MovementInputValue {get{return _movementInputValue;}}

    public float DashSpeed {get{return _dashSpeed;} set{_dashSpeed = value;}}
    private void Awake() 
    {
        //Setup State
        _states = new PlayerStateFactory(this);
        _currentState = _states.Idle();
        _currentState.EnterState();

        //Setup Player Variables
        _rb = GetComponent<Rigidbody2D>();
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


    public void OnMoveButton(InputAction.CallbackContext context)
    {
        _movementInputValue = context.ReadValue<Vector2>();
        _isMovePressed = _movementInputValue.magnitude != 0;
        //Debug.Log("MOVE Button Is Pressed");
    }
    public void OnDashButton(InputAction.CallbackContext context)
    {
        _isDashPressed = context.ReadValueAsButton();
        //Debug.Log("DASH Button Is Pressed");
    }
    public void OnShootButton(InputAction.CallbackContext context)
    {
        _isShootPressed = context.ReadValueAsButton();
        //Debug.Log("SHOOT Button Is Pressed");
    }
    public void OnAttackButton(InputAction.CallbackContext context)
    {
        _isAttackPressed = context.ReadValueAsButton();
        //Debug.Log("ATTACK Button Is Pressed");
    }
}
