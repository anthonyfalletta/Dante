public abstract class ProjectileBaseState
{
    private bool _isRootState = false;
    private ProjectileStateMachine _ctx;
    private ProjectileStateFactory _factory;
    private ProjectileBaseState _currentSubState;
    private ProjectileBaseState _currentSuperState;

    protected bool IsRootState{set{_isRootState=value;}}
    protected ProjectileStateMachine Ctx{get{return _ctx;}}
    protected ProjectileStateFactory Factory{get{return _factory;}}
    
    public ProjectileBaseState(ProjectileStateMachine currentContext, ProjectileStateFactory projectileStateFactory){
        _ctx = currentContext;
        _factory = projectileStateFactory;
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

    protected void SwitchState(ProjectileBaseState newState){
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
    protected void SetSuperState(ProjectileBaseState newSuperState){
        _currentSuperState = newSuperState;
    }
    protected void SetSubState(ProjectileBaseState newSubState){
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
