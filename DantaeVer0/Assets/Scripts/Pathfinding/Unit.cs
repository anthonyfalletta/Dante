using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
	const float minPathUpdateTIme = 0.2f; 
	const float pathUpdateMoveThreshold = 0.5f;

	public Transform target;
	public float speed = 20;
	Vector3[] path;
	int targetIndex;

	void Start() {
		StartCoroutine(UpdatePath());
	}

   IEnumerator UpdatePath(){
		if(Time.timeSinceLevelLoad < 0.3f){
			yield return new WaitForSeconds(0.3f);
		}
		PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector3 targetPosOld = target.position;

	while (true){
		yield return new WaitForSeconds (minPathUpdateTIme);

		if ((target.position-targetPosOld).sqrMagnitude > sqrMoveThreshold){
			PathRequestManager.RequestPath(new PathRequest(transform.position,target.position, OnPathFound));
			targetPosOld = target.position;
		}	
	}
   }

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
			path = newPath;
			//targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath() {
		Vector3 currentWaypoint = path[0];

		while (true) {
			if (transform.position == currentWaypoint) {
				targetIndex ++;
				if (targetIndex >= path.Length) {
					targetIndex = 0;
					path = new Vector3[0];
					yield break;
				}
				currentWaypoint = path[targetIndex];
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
				Gizmos.DrawCube(path[i], new Vector3(1,1,5));

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}
