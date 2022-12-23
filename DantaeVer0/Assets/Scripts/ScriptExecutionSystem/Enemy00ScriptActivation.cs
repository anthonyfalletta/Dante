using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy00ScriptActivation : MonoBehaviour
{
    EnemyStateMachine _ctx;
    EnemyActions _actions;
    EnemyStat _stat;
    EnemyObject _obj;
    GameObject _player;
    Unit _unit;

    public EnemyStateMachine Ctx {get{return _ctx;} set{_ctx = value;}}
    public EnemyActions Actions {get{return _actions;} set{_actions=value;}}
    public EnemyStat Stat {get{return _stat;} set{_stat = value;}}
    public EnemyObject Obj {get{return _obj;} set{_obj = value;}}
    public Unit EnemyPahtfinding {get{return _unit;} set{_unit = value;}}

    private void Awake() {
        _ctx = this.GetComponent<EnemyStateMachine>();
        _actions = this.GetComponent<EnemyActions>();
        _stat = this.GetComponent<EnemyStat>();
        _obj = this.GetComponent<EnemyObject>();
        _unit = this.GetComponent<Unit>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _stat.StartActivation();
        _obj.StartActivation();
        _actions.StartActivation();
        _ctx.StartActivation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
