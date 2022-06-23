using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{


	public Transform target;
	public float speed = 20;
	public float turnSpeed = 3;
	public float turnDst = 5;
	//Vector3[] path;
	//int targetIndex;

	Path path;

	void Start() {
		PathRequestManager.RequestPath(transform.position,target.position, OnPathFound);
	}

	 private void Update() {
    //! Using Update to Recalculate if Target Moves
	//! Unit does not move exactly to player
    //*Need to have path updated during Update and not create queque so instead when change to path happens it is performed
    //*Update of path in conjuction with path update might be best
    Debug.Log(transform.position);
        if (Keyboard.current[Key.Space].wasPressedThisFrame)
        {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        }
   }

	//newPath is array of waypoints and changed
	public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
		if (pathSuccessful) {
			path = new Path(waypoints, transform.position, turnDst);
			//targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath() {
		bool followingPath = true;
		int pathIndex = 0;

		//Rotation to Look At First Waypoint 2D
		Vector3 initialAngle  = path.lookPoints[0]-transform.position;
		transform.rotation = Quaternion.LookRotation(initialAngle,Vector3.back);

		while (followingPath){
			Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
			while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D)){
				if (pathIndex == path.finishLineIndex){
					followingPath = false;
					break;
				}
				else{
					pathIndex++;
				}
			}	

			/*
			if (followingPath){
				Vector3 diffPath  = path.lookPoints[pathIndex]-transform.position;
				float rot_yPath = Mathf.Atan2(diffPath.y, diffPath.x) * Mathf.Rad2Deg;
				Quaternion rot_quat = Quaternion.Euler(rot_yPath+180f, 90f, -90f);;
				transform.rotation = Quaternion.Lerp(transform.rotation,rot_quat, Time.deltaTime*turnSpeed);
				transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
			}
			*/
			
			if (followingPath){
				
				//flipping at first causing problems
				Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position, Vector3.back);
				transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
			}
			

			yield return null;
		}

		//? Older instance of movement before smooth pathing
		//Vector3 currentWaypoint = path[0];
		//while (true) {
			
			/*
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
			//transform.position = Vector3.MoveTowards(transform.position,currentWaypoint,speed * Time.deltaTime);		
			//yield return null;

		//}
	}

	public void OnDrawGizmos() {
		if (path != null){
			path.DrawWithGizmos();
		}
		/*
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
		*/
	}
}
