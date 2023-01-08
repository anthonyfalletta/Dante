using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "StateSystem/State")]
public class State : ScriptableObject
{

    public StateAction[] actions;
    public StateTransition[] transitions;

    public Color sceneGizmoColor = Color.grey;

    public void EnterState(StateController controller){
        DoStartActions(controller);
    }

    public void UpdateState(StateController controller){
        DoUpdateActions(controller);
        CheckTransitions(controller);
    }

    public void ExitState(StateController controller){
        controller.stateTimeElapsed = 0;
        //controller.StopAllCoroutines();
    }

    private void DoStartActions(StateController controller){
        for(int i=0; i<actions.Length; i++){
            actions[i].ActStart(controller);
        }
    }

    private void DoUpdateActions(StateController controller){
        for(int i=0; i<actions.Length; i++){
            actions[i].ActUpdate(controller);
        }
    }

    private void CheckTransitions(StateController controller){
        for(int i=0; i<transitions.Length; i++){
            bool decisionSucceeded = transitions[i].decision.Decide(controller);

            if (decisionSucceeded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }
}
