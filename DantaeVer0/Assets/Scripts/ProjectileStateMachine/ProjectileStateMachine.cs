using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileStateMachine : MonoBehaviour
{
    ProjectileInputs projectileInputs;


    //State Variables
    ProjectileBaseState _currentState;
    ProjectileStateFactory _states;

    //Abilities Variables
    bool _isShootPressed;
    bool _isSpecialPressed;

    //Projectile Variables
    GameObject _projectilePrefab;
    Rigidbody2D _projectileRb;
    GameObject _projectileGO;

    //Player Variables
    Rigidbody2D _playerRb;
    GameObject _playerGO;

    Vector2 _movementInputValue;

    float _shootingDistance;
    Vector2 _shootingPos;
    RaycastHit2D _raycastHit2D;
    LayerMask _playerLayerMask;
    Vector3 _raycastPos;


    Collision2D _isCollision;
    Collider2D _isCollider;
    Vector3 _targetPos;

    bool _specialWait;

    //Getters & Setters
    public ProjectileBaseState CurrentState{get{return _currentState;} set{_currentState = value;}}
    public bool IsShootPressed {get{return _isShootPressed;} set{_isShootPressed = value;}}
    public bool IsSpecialPressed {get{return _isSpecialPressed;} set{_isSpecialPressed = value;}}

    public Rigidbody2D ProjectileRb {get{return _projectileRb;} set{_projectileRb = value;}}
    public GameObject ProjectileGO {get{return _projectileGO;} set{_projectileGO = value;}}
    public GameObject ProjectilePrefab {get{return _projectilePrefab;} set{_projectilePrefab = value;}}

    public Rigidbody2D PlayerRb {get{return _playerRb;} set{_playerRb = value;}}
    public GameObject PlayerGO {get{return _playerGO;} set{_playerGO = value;}}

    public Vector2 MovementInputValue {get{return _movementInputValue;}}

    public float ShootingDistance {get{return _shootingDistance;} set{_shootingDistance = value;}}
    public Vector2 ShootingPos {get{return _shootingPos;} set{_shootingPos = new Vector2(value.x, value.y);}}
    public RaycastHit2D RaycastHit2D {get{return _raycastHit2D;} set{_raycastHit2D = value;}}
    public LayerMask PlayerLayerMask {get{return _playerLayerMask;} set{_playerLayerMask = value;}}
    public Vector3 RaycastPos {get{return _raycastPos;} set{_raycastPos = new Vector3(value.x,value.y,value.z);}}

    
    public Collision2D IsCollision{get{return _isCollision;} set{_isCollision = value;}}
    public Collider2D IsCollider{get{return _isCollider;} set{_isCollider = value;}}
    public Vector3 TargetPos {get{return _targetPos;} set{_targetPos = new Vector2(value.x,value.y);}}

    public bool SpecialWait {get{return _specialWait;} set{_specialWait = value;}}

    private void Awake() 
    {
        projectileInputs = GameObject.Find("ProjectileManagerInputs").GetComponent<ProjectileInputs>();

        //Setup State
        _states = new ProjectileStateFactory(this);
        _currentState = _states.Held();
        _currentState.EnterState();

        //Setup Player Variables
        _playerGO = GameObject.Find("Player");
        _playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();

        _projectileGO = this.gameObject;
        _projectileRb = _projectileGO.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _projectilePrefab = (GameObject)Resources.Load("Projectile");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInputs();
        _currentState.UpdateStates();
        Debug.Log("Current Projectile State: " + _currentState);
        Debug.Log("Collider: " + _isCollider);
    }

    public void UpdateInputs()
    {
        _movementInputValue = projectileInputs.MovementInputValue;
        _isShootPressed = projectileInputs.IsShootPressed;
        _isSpecialPressed = projectileInputs.IsSpecialPressed;
    }

    public void ProjectileDestroy()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision2D) {
        Debug.Log("Projectile experienced collision!");
        _isCollision = collision2D;
    }

    private void OnTriggerEnter2D(Collider2D collider2D) {
        _isCollider = collider2D;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_raycastPos, 0.1f);
    }
}
