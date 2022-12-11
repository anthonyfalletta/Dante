using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
	NodeGrid grid;

	const float minPathUpdateTIme = 0.25f; 
	const float pathUpdateMoveThreshold = 1.0f;
	public bool followPath;

	public Vector3 target;
	public float speed = 0;
	Vector3[] path;
	int targetIndex;

	//public Vector3 Target{get{return target;} set{target = value;}}

	private void Awake() {
		grid = GameObject.Find("PathfindingManager").GetComponent<NodeGrid>();
		
	}

	void Start() {
		//TODO Implement toggle method for follow path where when true call startCoroutine and false calls stopCoroutine
			//target = GameObject.Find("Player").transform.position;
			//ToggleUnitFollow();

			/*
			//float randomX = Random.Range(2.0f,8.0f);
			//float randomY = Random.Range(2.0f,8.0f);
			float randomX = 0.0f;
			float randomY = 3.0f;
			Vector3 randomNode = new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y,this.gameObject.transform.position.z) + new Vector3(randomX,randomY,0);

			Node targetTestNode = grid.NodeFromWorldPoint(randomNode);
			if(targetTestNode.walkable)
				target = randomNode;
			else{
				Debug.Log("Target is not walkable");
			}
			*/
	}

	private void Update() {
		//target = GameObject.Find("Player").transform.position;
		
	}


	public void SetTarget(Vector3 targetPosition, float unitSpeed)
	{
		target = targetPosition;
		speed = unitSpeed;
	}

	public bool CheckIfTargetWalkable(Vector3 targetPosition){
		Node targetTestNode = grid.NodeFromWorldPoint(targetPosition);
			if(targetTestNode.walkable)
				return true;
			else{
				return false;
			}
	}

	public bool CheckIfMeetsPathThresholdMoveUpdate(Vector3 oldTargePosition, Vector3 newTargetPosition){
		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		
		if ((newTargetPosition-oldTargePosition).sqrMagnitude > sqrMoveThreshold){
			return true;
		}else{
			return false;
		}
	}


	public void ToggleUnitFollow(){
	if(!followPath){
		followPath = true;
		StartCoroutine(UpdatePath());
	}
	else if(followPath)
	{
		StopCoroutine(UpdatePath());
		followPath = false;
	}
   }

	public void ActivateUnitFollow(){
		if(!followPath){
			followPath = true;
			StartCoroutine(UpdatePath());
		}
		
   	}

	public void DeactivateUnitFollow(){
		if(followPath){
			followPath = false;
			StopCoroutine(UpdatePath());
		}
		
   	}

   IEnumerator UpdatePath(){
		if(Time.timeSinceLevelLoad < 0.3f){
			yield return new WaitForSeconds(0.3f);
		}
		if (followPath){
			PathRequestManager.RequestPath(new PathRequest(transform.position, target, OnPathFound));
		}
		

		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector3 targetPosOld = target;

	while (true){
		yield return new WaitForSeconds (minPathUpdateTIme);

		if ((target-targetPosOld).sqrMagnitude > sqrMoveThreshold){
			if(followPath){
				PathRequestManager.RequestPath(new PathRequest(transform.position,target, OnPathFound));
			}
			targetPosOld = target;
		}	
	}
   }

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
			path = newPath;
			//targetIndex = 0;
			StopCoroutine("FollowPath");
			//StopCoroutine("FollowPath");
			//StopCoroutine("FollowPath");
			//StopCoroutine("FollowPath");
			//StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath() {
		Vector3 currentWaypoint = path[0];
		targetIndex = 0;

		while (true) {
			if (transform.position == currentWaypoint) {
				targetIndex ++;
				if (targetIndex >= path.Length) {
					targetIndex = 0;
					path = new Vector3[0];
					yield break;
				}
				currentWaypoint = path[targetIndex];
				//Debug.Log("Current Waypoint:" + currentWaypoint);
			}

			//Unit Direction Towards Waypoints
			/*
			//! Fix having right or x local axis move towards target
            //Rotate Towards Waypoint
            Debug.Log(currentWaypoint + " waypoint position");
            Vector3 targetDir = currentWaypoint - transform.position;
            Debug.Log(targetDir + " target dir");
            float step = rotationSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            Debug.DrawRay(transform.position, newDir, Color.red);
            transform.rotation = Quaternion.LookRotation(newDir,-Vector3.forward);
            */

			//Unit Movement Towards Waypoints
			transform.position = Vector3.MoveTowards(transform.position,currentWaypoint,speed * Time.deltaTime);
			yield return null;

		}
	}

	public void OnDrawGizmos() {

		Gizmos.DrawSphere(target, 0.20f);

		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(new Vector3(path[i].x,path[i].y,-5), new Vector3(0.25f,0.25f,-5));

				if (i == targetIndex) {
					Gizmos.DrawLine(new Vector3(transform.position.x,transform.position.y,-5), new Vector3(path[i].x, path[i].y,-5));
				}
				else {
					Gizmos.DrawLine(new Vector3(path[i-1].x,path[i-1].y,-5),new Vector3(path[i].x, path[i].y,-5));
				}
			}
		}
	}
}
