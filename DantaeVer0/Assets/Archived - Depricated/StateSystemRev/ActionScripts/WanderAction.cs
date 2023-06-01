using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu (menuName = "StateSystem/Actions/Wander")]
public class WanderAction : StateAction
{

    public override void ActStart(StateController controller)
    {
        controller.data.bWanderComplete = false;
        controller.data.wanderStartPoint = controller.gameObject.transform.position;
        StateController.instance.StartCoroutine(Wander(controller));   
    }

    public override void ActUpdate(StateController controller)
    {
        StateController.instance.StartCoroutine(CheckWanderComplete(controller));
    }
   
    private IEnumerator Wander(StateController controller)
    {
        Debug.Log("Wandering with Enumerator: " + controller.gameObject);
        //Debug.Log("Start Wander");
        
        controller.data.wanderPoint = RandomPointInAnnulus(controller.data.wanderStartPoint, controller.data.MinWander.Value, controller.data.MaxWander.Value);

        while (CheckIfRandomPointWalkable(controller, controller.data.wanderPoint) == false)
        {
            controller.data.wanderPoint = RandomPointInAnnulus(controller.data.wanderStartPoint, controller.data.MinWander.Value, controller.data.MaxWander.Value);
            yield return null;
        }

        //Debug.Log("Walkable Wander Target: " + controller.wanderPoint);
        controller.pathfinding.ActivateUnitFollow();
        controller.pathfinding.SetTarget(controller.data.wanderPoint, controller.data.Speed.Value);
        controller.data.wanderPreviousPoint = controller.data.wanderPoint;
        Debug.Log("Bool Switch True: " + controller.gameObject);
        controller.data.bWanderComplete = true;
    }

    private Vector2 RandomPointInAnnulus(Vector2 origin, float minRadius, float maxRadius){
        var randomDirection = (UnityEngine.Random.insideUnitCircle * origin).normalized;
        var randomDistance = UnityEngine.Random.Range(minRadius, maxRadius);
 
        var point = origin + randomDirection * randomDistance;
 
        return point;
    }

    private bool CheckIfRandomPointWalkable(StateController controller, Vector3 wanderPoint)
    {    
        if (controller.pathfinding.CheckIfTargetWalkable(wanderPoint) && controller.pathfinding.CheckIfMeetsPathThresholdMoveUpdate(controller.data.wanderPreviousPoint, wanderPoint))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator CheckWanderComplete(StateController controller)
    {
        if (Vector2.Distance(controller.data.wanderPoint,controller.gameObject.transform.position) < 0.05f && controller.data.bWanderComplete == true)
        {
            Debug.Log("Wander Complete, Redo Wander: "+ controller.gameObject);
            controller.data.bWanderComplete = false;
            yield return new WaitForSeconds(5.0f);  
            StateController.instance.StartCoroutine(Wander(controller));             
        }
        yield return null;
    }

}
