using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public State currentState;
    private bool aiActive;
    public State remainState;
    public float stateTimeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        aiActive = true;
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
            currentState = nextState;
        }
    }

    public bool CheckIfCountdownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }
}
