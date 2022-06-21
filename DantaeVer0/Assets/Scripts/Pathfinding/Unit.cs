using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{


	public Transform target;
	float speed = 20;
	Vector3[] path;
	int targetIndex;

	void Start() {
		PathRequestManager.RequestPath(transform.position,target.position, OnPathFound);
	}

	 private void Update() {
    //! Using Update to Recalculate if Target Moves
	//! Unit does not move exactly to player
    //*Need to have path updated during Update and not create queque so instead when change to path happens it is performed
    //*Update of path in conjuction with path update might be best
    
        if (Keyboard.current[Key.Space].wasPressedThisFrame)
        {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
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
