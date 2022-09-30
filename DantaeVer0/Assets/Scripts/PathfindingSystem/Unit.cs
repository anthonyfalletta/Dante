using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
	const float minPathUpdateTIme = 0.25f; 
	const float pathUpdateMoveThreshold = 1.0f;
	public bool followPath;

	public Vector3 target;
	public float speed = 20;
	Vector3[] path;
	int targetIndex;

	void Start() {
		//TODO Implement toggle method for follow path where when true call startCoroutine and false calls stopCoroutine
		if (target == null){
			target = GameObject.Find("Player").transform.position;
			//float randomX = Random.Range(2.0f,8.0f);
			//float randomY = Random.Range(2.0f,8.0f);
			//target = new Vector3(this.gameObject.transform.position.x + randomX,this.gameObject.transform.position.y + randomY,this.gameObject.transform.position.z);
		}
	}

	private void Update() {
		target = GameObject.Find("Player").transform.position;
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
			StopCoroutine("FollowPath");
			StopCoroutine("FollowPath");
			StopCoroutine("FollowPath");
			StopCoroutine("FollowPath");
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
