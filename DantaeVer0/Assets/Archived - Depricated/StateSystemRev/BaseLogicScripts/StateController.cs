using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public CharactersStat data;
    public Unit pathfinding;

    public State currentState;
    private bool aiActive;
    public State remainState;
    public float stateTimeElapsed;
    

    
    


    public static StateController instance;

    private void Awake() 
    {
        data = this.gameObject.GetComponent<CharactersStat>();
        pathfinding = this.gameObject.GetComponent<Unit>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StateController.instance = this;
        
        aiActive = true;
        //wanderStartPoint = this.gameObject.transform.position;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);
    }

    void OnDrawGizmos()
    {
        if (currentState != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
        }
    }

    public void TransitionToState(State nextState)
    {
        if(nextState != remainState){
            currentState.ExitState(this);
            currentState = nextState;
            currentState.EnterState(this);
        }
    }

    public bool CheckIfCountdownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }
}
