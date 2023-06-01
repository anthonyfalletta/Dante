﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState
{
    private bool _isRootState = false;
    private EnemyStateMachine _ctx;
    private EnemyStateFactory _factory;
    private EnemyBaseState _currentSubState;
    private EnemyBaseState _currentSuperState;

    protected bool IsRootState{set{_isRootState=value;}}
    protected EnemyStateMachine Ctx{get{return _ctx;}}
    protected EnemyStateFactory Factory{get{return _factory;}}
    
    public EnemyBaseState(EnemyStateMachine currentContext, EnemyStateFactory playerStateFactory){
        _ctx = currentContext;
        _factory = playerStateFactory;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();
    public abstract void InitializeSubState();

    public void UpdateStates(){
        UpdateState();
        if (_currentSubState != null)
        {
            _currentSubState.UpdateStates();
        }
    }

    public void FixedUpdateStates()
    {
        FixedUpdateState();
        if (_currentSubState != null)
        {
            _currentSubState.FixedUpdateStates();
        }
    }
    
    public void ExitStates()
    {
        ExitState();
        if (_currentSubState != null)
        {
            _currentSubState.ExitStates();
        }
    }

    public void SwitchState(EnemyBaseState newState){
        //current state exits state
        ExitState();

        //new state state enters state
        newState.EnterState();

        if (_isRootState)
        {
            _ctx.CurrentState = newState;
        }
        else if (_currentSuperState != null)
        {
            _currentSuperState.SetSubState(newState);
        }
    }
    protected void SetSuperState(EnemyBaseState newSuperState){
        _currentSuperState = newSuperState;
    }
    protected void SetSubState(EnemyBaseState newSubState){
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
