using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerStateMachine : MonoBehaviour
{
    //State Variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    
    

    bool _dashStateEnabled;

    //Getters & Setters
    public PlayerBaseState CurrentState{get{return _currentState;} set{_currentState = value;}}
    public PlayerStateFactory States{get{return _states;} set{_states = value;}}


    
    public bool DashStateEnabled {get{return _dashStateEnabled;} set{_dashStateEnabled = value;}}




    //PlayerActions _action;
    //public PlayerActions Action {get{return _action;} set{_action = value;}}
    public Rigidbody2D PlayerRb {get{return _playerRb;} set{_playerRb = value;}}


    
    PlayerStat _stat;
    PlayerInputController _input;
    PlayerObject _obj;
    PlayerAnimationController _animator;
    public PlayerStat Stat {get{return _stat;} set{_stat = value;}}
    public PlayerInputController Input {get{return _input;} set{_input = value;}}
    public PlayerObject Obj {get{return _obj;} set{_obj = value;}}
    public PlayerAnimationController Animator {get{return _animator;} set{_animator = value;}}


    Rigidbody2D _playerRb;

    private void Awake() 
    {
        _playerRb = GetComponent<Rigidbody2D>();

        //Setup State
        _states = new PlayerStateFactory(this);
        _currentState = _states.Idle();
        _currentState.EnterState();

        //_action = this.GetComponent<PlayerActions>();
        _stat = this.GetComponent<PlayerStat>();
        _input = this.GetComponent<PlayerInputController>();
        _obj = this.GetComponent<PlayerObject>();
        _animator = this.GetComponent<PlayerAnimationController>();
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


    public void DashSpeedSlowdown(){
        StartCoroutine(IDashSpeedSlowdown());
    }

    IEnumerator IDashSpeedSlowdown()
    {
        while(Stat.DashSpeed.Value > Stat.DashDuration.Value)
        {
            Stat.DashSpeed.BaseValue -= Stat.DashSpeed.Value * Stat.DashDecrease.Value * Time.deltaTime;
            yield break;
        }
        StartCoroutine(ICooldownTimer(Stat.DashCooldown.Value));
        CurrentState.SwitchState(States.Move());    
    }

    IEnumerator ICooldownTimer(float dashCooldownTime)
    {
            yield return new WaitForSeconds(dashCooldownTime);
            DashStateEnabled = true;
    }

    public void AttackCo(Vector3 position, Quaternion rotation){
        StartCoroutine((IAttackSwipe(position, rotation)));
    }

    IEnumerator IAttackSwipe(Vector3 position, Quaternion rotation)
    {
        InstantiateAttackGameObject();
        yield return new WaitForSeconds(2f);
    }

    public void InstantiateAttackGameObject()
    {
        float angle = Mathf.Atan2(Input.LastMovementInputValue.normalized.y,Input.LastMovementInputValue.normalized.x) * Mathf.Rad2Deg;
        Quaternion playerRot = Quaternion.Euler(0,0,angle);
        Vector3 playerPos = Obj.PlayerGO.transform.position + (new Vector3(Input.LastMovementInputValue.x,Input.LastMovementInputValue.y,0).normalized * 0.1f);
        Instantiate(Obj.AttackObject, playerPos, playerRot);
    }

    public void InstantiateProj(Vector3 playerPos, Quaternion playerRot){
        Instantiate(Obj.ProjectilePrefab, playerPos, playerRot);
    }
}
