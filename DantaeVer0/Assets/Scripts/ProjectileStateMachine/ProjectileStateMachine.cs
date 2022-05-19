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

    //Projectile Variables
    Rigidbody2D _rb;
    GameObject _GO;

    Vector2 _movementInputValue;

    float _shootingDistance;
    Vector2 _shootingPos;
    RaycastHit2D _raycastHit2D;
    LayerMask _playerLayerMask;
    Vector3 _raycastPos;

    //Getters & Setters
    public ProjectileBaseState CurrentState{get{return _currentState;} set{_currentState = value;}}
    public bool IsShootPressed {get{return _isShootPressed;} set{_isShootPressed = value;}}

    public Rigidbody2D Rb {get{return _rb;} set{_rb = value;}}
    public GameObject GO {get{return _GO;} set{_GO = value;}}

    public Vector2 MovementInputValue {get{return _movementInputValue;}}

    public float ShootingDistance {get{return _shootingDistance;} set{_shootingDistance = value;}}
    public Vector2 ShootingPos {get{return _shootingPos;} set{_shootingPos = new Vector2(value.x, value.y);}}
    public RaycastHit2D RaycastHit2D {get{return _raycastHit2D;} set{_raycastHit2D = value;}}
    public LayerMask PlayerLayerMask {get{return _playerLayerMask;} set{_playerLayerMask = value;}}
    public Vector3 RaycastPos {get{return _raycastPos;} set{_raycastPos = new Vector3(value.x,value.y,value.z);}}

    

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

    public void OnMoveButton(InputAction.CallbackContext context)
    {
        Debug.Log("Projectile MOVE button PRESSED");
        _movementInputValue = context.ReadValue<Vector2>();
    }

    public void OnShootButton(InputAction.CallbackContext context)
    {
        Debug.Log("Shoot BUtton:" + _isShootPressed);
        _isShootPressed = context.ReadValueAsButton();
        
    }

    public void ProjectileDestroy()
    {
        Debug.Log("Projectile is destroyed");
        Destroy(this.gameObject);
    }
}
