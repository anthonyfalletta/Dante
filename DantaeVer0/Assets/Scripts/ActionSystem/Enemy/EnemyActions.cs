using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyActions : MonoBehaviour
{
    EnemyStateMachine _ctx;
    EnemyStat _stat;
    //EnemyObject _obj;

    GameObject _player;

    Unit _unit;
    Vector3 wanderPoint;
    Vector3 wanderStartPoint;
    Vector3 wanderPreviousPoint;

    bool bWanderComplete;

    public EnemyStateMachine Ctx {get{return _ctx;} set{_ctx = value;}}
        public EnemyStat Stat {get{return _stat;} set{_stat = value;}}
    //public EnemyObject Obj {get{return _obj;} set{_obj = value;}}

    public Unit EnemyPahtfinding{get{return _unit;} set{_unit = value;}}

    private void Awake() {
        _player = GameObject.Find("Player");

        _ctx = this.GetComponent<EnemyStateMachine>();
        _stat = this.GetComponent<EnemyStat>();
        //_obj = this.GetComponent<PlayerObject>();
        _unit = this.GetComponent<Unit>();
    }

    void Start()
    {
        Debug.Log("They are linked");
        EnemyMovementZeroState.wandering += Wander;
        EnemyMovementZeroState.wanderingCheck += CheckWanderComplete;
        EnemyMovementZeroState.seek += Seek;
        EnemyMovementOneState.follow += Follow;
    
        wanderStartPoint = this.gameObject.transform.position;
        wanderPreviousPoint = this.gameObject.transform.position;
        Wander();

        bWanderComplete = true;
    }
    

    void Update()
    {

    }

    void Seek(){
        if (Vector2.Distance(_player.transform.position,this.gameObject.transform.position) < 4.0f)
        {
            Debug.Log("SWITCH TO MOVE2");
            Ctx.CurrentState.SwitchState(Ctx.States.MovementOne());
        }
    }

    void Wander(){
        wanderPoint = RandomPointInAnnulus(wanderStartPoint, Stat.MinWander.Value, Stat.MaxWander.Value);
        //Debug.Log("Wander Point: " + wanderPoint);
        if (EnemyPahtfinding.CheckIfTargetWalkable(wanderPoint) && EnemyPahtfinding.CheckIfMeetsPathThresholdMoveUpdate(wanderPreviousPoint, wanderPoint))
        {
            Debug.Log("Walkable Wander Target: " + wanderPoint);
            EnemyPahtfinding.ActivateUnitFollow();
            EnemyPahtfinding.SetTarget(wanderPoint, Stat.Speed.Value);
            wanderPreviousPoint = wanderPoint;
        }
        else
        {
            Debug.Log("Unwalkable Wander Target: " + wanderPoint);
            Wander();
        }
    }

    void CheckWanderComplete()
    {
        if (Vector2.Distance(wanderPoint,this.gameObject.transform.position) < 0.01f && bWanderComplete)
        {
            bWanderComplete = false;
            Debug.Log("Wander Complete, Redo Wander");
            StartCoroutine(FunctionWait(Wander,2.0f,bWanderComplete));
            
        }
    }

    void Follow(){
        Debug.Log("Follow Player");
        EnemyPahtfinding.SetTarget(_player.transform.position, Stat.Speed.Value);
        
    }

    public Vector2 RandomPointInAnnulus(Vector2 origin, float minRadius, float maxRadius){
 
        var randomDirection = (UnityEngine.Random.insideUnitCircle * origin).normalized;
 
        var randomDistance = UnityEngine.Random.Range(minRadius, maxRadius);
 
        var point = origin + randomDirection * randomDistance;
 
        return point;
    }

    IEnumerator FunctionWait(Action Method, float seconds, bool boolean)
    {
        yield return new WaitForSeconds(seconds);    

        Debug.Log("Wait is over");
        Method();
        
        bWanderComplete = true;
    }
}
