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
    bool _isSpecialPressed;
    
    //Player Variable
    Rigidbody2D _playerRb;
    GameObject _playerGO;

    //Movement Variables
    Vector2 _movementInputValue;
    Vector2 _lastMovementInputValue;
    
    float _dashSpeed;

    Coroutine _currentAttackResetRoutine;
    GameObject _attackPrefab;
    
    GameObject _projectilePrefab;

    Animator _animator;


    //Getters & Setters
    public PlayerBaseState CurrentState{get{return _currentState;} set{_currentState = value;}}
    public bool IsMovePressed {get{return _isMovePressed;} set{_isMovePressed = value;}}
    public bool IsDashPressed {get{return _isDashPressed;} set{_isDashPressed = value;}}
    public bool IsShootPressed {get{return _isShootPressed;} set{_isShootPressed = value;}}
    public bool IsAttackPressed {get{return _isAttackPressed;} set{_isAttackPressed = value;}}
    public bool IsSpecialPressed {get{return _isSpecialPressed;} set{_isSpecialPressed = value;}}

    public Rigidbody2D PlayerRb {get{return _playerRb;} set{_playerRb = value;}}
    public GameObject PlayerGO {get{return _playerGO;} set{_playerGO = value;}}

    public Vector2 MovementInputValue {get{return _movementInputValue;}}
    public Vector2 LastMovementInputValue {get{return _lastMovementInputValue;}}

    public float DashSpeed {get{return _dashSpeed;} set{_dashSpeed = value;}}

    public Coroutine AttackResetRoutine {get{return _currentAttackResetRoutine;} set{_currentAttackResetRoutine = value;}}
    public GameObject AttackObject {get{return _attackPrefab;} set{_attackPrefab = value;}}


    public GameObject ProjectilePrefab {get{return _projectilePrefab;} set{_projectilePrefab = value;}}

    public Animator Animator {get{return _animator;} set{_animator = value;}}

    private void Awake() 
    {
        //Setup State
        _states = new PlayerStateFactory(this);
        _currentState = _states.Idle();
        _currentState.EnterState();

        //Setup Player Variables
        _playerGO = this.gameObject;
        _playerRb = GetComponent<Rigidbody2D>();

        _animator = GetComponent<Animator>();
        
    }
    


    // Start is called before the first frame update
    void Start()
    {
        _projectilePrefab = (GameObject)Resources.Load("Projectile");
        _attackPrefab = (GameObject)Resources.Load("AttackSwipeField");

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
        if (_movementInputValue != Vector2.zero)
        {
            _lastMovementInputValue = _movementInputValue;
        }
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
    public void OnSpecialButton(InputAction.CallbackContext context)
    {
        _isSpecialPressed = context.ReadValueAsButton();
        //Debug.Log("ATTACK Button Is Pressed");
    }


    public void CreateAttackGameObject(Vector3 position, Quaternion rotation)
    {
        Debug.Log("CreateAttackObjectScript Running");
        Instantiate(_attackPrefab, position,rotation);
    }

        
    public void InstantiateProjectile()
    {
        Instantiate(_projectilePrefab, _playerGO.transform.position, _playerGO.transform.rotation);
    }
}
