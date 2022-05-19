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
    GameObject _GO;

    Vector2 _movementInputValue;
    //Movement Variables
    float _dashSpeed;

    float _shootingDistance;
    GameObject _projectilePrefab;
    GameObject _projectileGO;
    Rigidbody2D _projectileRB;
    Vector2 _shootingPos;
    RaycastHit2D _raycastHit2D;
    LayerMask _playerLayerMask;
    Vector3 _raycastPos;

    Coroutine _currentAttackResetRoutine;
    GameObject _attackPrefab;
    

    //Getters & Setters
    public PlayerBaseState CurrentState{get{return _currentState;} set{_currentState = value;}}
    public bool IsMovePressed {get{return _isMovePressed;} set{_isMovePressed = value;}}
    public bool IsDashPressed {get{return _isDashPressed;} set{_isDashPressed = value;}}
    public bool IsShootPressed {get{return _isShootPressed;} set{_isShootPressed = value;}}
    public bool IsAttackPressed {get{return _isAttackPressed;} set{_isAttackPressed = value;}}

    public Rigidbody2D Rb {get{return _rb;} set{_rb = value;}}
    public GameObject GO {get{return _GO;} set{_GO = value;}}

    public Vector2 MovementInputValue {get{return _movementInputValue;}}

    public float DashSpeed {get{return _dashSpeed;} set{_dashSpeed = value;}}

    public float ShootingDistance {get{return _shootingDistance;} set{_shootingDistance = value;}}
    public GameObject ProjectilePrefab {get{return _projectilePrefab;} set{_projectilePrefab = value;}}
    public GameObject ProjectileGO {get{return _projectileGO;} set{_projectileGO = value;}}
    public Rigidbody2D ProjectileRB {get{return _projectileRB;} set{_projectileRB = value;}}
    public Vector2 ShootingPos {get{return _shootingPos;} set{_shootingPos = new Vector2(value.x, value.y);}}
    public RaycastHit2D RaycastHit2D {get{return _raycastHit2D;} set{_raycastHit2D = value;}}
    public LayerMask PlayerLayerMask {get{return _playerLayerMask;} set{_playerLayerMask = value;}}
    public Vector3 RaycastPos {get{return _raycastPos;} set{_raycastPos = new Vector3(value.x,value.y,value.z);}}

    public Coroutine AttackResetRoutine {get{return _currentAttackResetRoutine;} set{_currentAttackResetRoutine = value;}}
    public GameObject AttackObject {get{return _attackPrefab;} set{_attackPrefab = value;}}

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
        _attackPrefab = (GameObject)Resources.Load("AttackSwipeField");
        _projectilePrefab = (GameObject)Resources.Load("Projectile");

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



    public void CreateAttackGameObject(Vector3 position, Quaternion rotation)
    {
        Debug.Log("CreateAttackObjectScript Running");
        Instantiate(_attackPrefab, position,rotation);
    }

    public void InstantiateProjectile()
    {
        _projectileGO = Instantiate(_projectilePrefab, transform.position, transform.rotation);
        //_projectileRB = _projectileGO.GetComponent<Rigidbody2D>();
    }
}
