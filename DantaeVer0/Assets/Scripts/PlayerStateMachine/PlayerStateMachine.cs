using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.ObjectModel;

public class PlayerStateMachine : MonoBehaviour
{
    //State Variables
    
    PlayerBaseState _currentState;
    PlayerStateFactory _states;
    PlayerStat _stat;

    //!
    //public CharacterStat Speed;

    //Optimized Animation variables
    int _isMovingHash;
    int _isDashingHash;

    //Abilities Variables
    bool _isMovePressed;
    bool _isDashPressed;
    bool _isShootPressed;
    bool _isAttackPressed;
    bool _isSpecialPressed;
    bool _isSpecialActive;
    
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
    public PlayerStateFactory States{get{return _states;} set{_states = value;}}

    public int IsMovingHash{get{return _isMovingHash;} set{_isMovingHash = value;}}
    public int IsDashingHash{get{return _isDashingHash;} set{_isDashingHash = value;}}

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
        //Setup Optimized Hash Animations
        AnimationHashing();
        
        _stat = this.GetComponent<PlayerStat>();

        //Setup State
        _states = new PlayerStateFactory(this, _stat);
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
        _lastMovementInputValue = new Vector2(0,-1);

        //!
        //Speed.BaseValue = 100f;

    }

    // Update is called once per frame
    void Update()
    {
        HandleMoveAnimation();
        _currentState.UpdateStates();
        Debug.Log("Current State: " + CurrentState);

         
    }

    void FixedUpdate() {
        _currentState.FixedUpdateStates();
    }

    void AnimationHashing()
    {
        _isMovingHash = Animator.StringToHash("isMoving");
        _isDashingHash = Animator.StringToHash("isDashing");
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

    public void HandleMoveAnimation()
    {
        _animator.SetFloat("Horizontal", _movementInputValue.x);
        _animator.SetFloat("Vertical", _movementInputValue.y);
        _animator.SetFloat("Magnitude", _movementInputValue.sqrMagnitude);
        _animator.SetFloat("LastHorizontal", _lastMovementInputValue.x);
        _animator.SetFloat("LastVertical", _lastMovementInputValue.y);       
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


    public void InstantiateAttackGameObject()
    {
        float angle = Mathf.Atan2(_lastMovementInputValue.normalized.y,_lastMovementInputValue.normalized.x) * Mathf.Rad2Deg;
        Quaternion playerRot = Quaternion.Euler(0,0,angle);
        Vector3 playerPos = _playerGO.transform.position + (new Vector3(_lastMovementInputValue.x,_lastMovementInputValue.y,0).normalized * 0.1f);
        Instantiate(_attackPrefab, playerPos, playerRot);
    }

        
    public void InstantiateProjectile()
    {
        float angle = Mathf.Atan2(_lastMovementInputValue.normalized.y,_lastMovementInputValue.normalized.x) * Mathf.Rad2Deg;
        Quaternion playerRot = Quaternion.Euler(0,0,angle);
        Vector3 playerPos = _playerGO.transform.position + (new Vector3(-_lastMovementInputValue.x,-_lastMovementInputValue.y,0).normalized * 0.1f);
        Instantiate(_projectilePrefab, playerPos, playerRot);
    }
}
