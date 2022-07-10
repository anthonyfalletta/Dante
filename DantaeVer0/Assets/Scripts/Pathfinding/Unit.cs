using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
	//!Big Problem With Hitting/Cutting Corners
	//!If 3d need to fix axis of top rotation to constraint
	//!Will need to change path smoothing and simplifying perhaps have nodes points altered as floating parameters


	public Transform target;
	public bool pathView;
	public float speed = 5;
	public float turnSpeed = 8;
	public float turnDst = 8;
	//Vector3[] path;
	//int targetIndex;

	Path path;

	const float pathUpdateMoveThreshold = 0.5f;
	const float minPathUpdateTime = 0.2f;

	Rigidbody rb;

	private void Awake() {
		rb = GetComponent<Rigidbody>();
	}

	void Start() {
		StartCoroutine(UpdatePath());
	}

	private void Update() {
		Debug.Log(transform.position);
	}

	IEnumerator UpdatePath(){
		
		if (Time.timeSinceLevelLoad < .3f){
			yield return new WaitForSeconds(.3f);
		}
		PathRequestManager.RequestPath(new PathRequest(transform.position,target.position,OnPathFound));

		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector3 targetPosOld = target.position;

		while(true){
			yield return new WaitForSeconds (minPathUpdateTime);
			if((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
			{
				PathRequestManager.RequestPath(new PathRequest(transform.position,target.position,OnPathFound));
				targetPosOld = target.position;
			}	
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

		//Rotation to Look At First Waypoint
		Vector3 initialAngle  = path.lookPoints[0]-transform.position;

		//Not Physics Rotation
		transform.rotation = Quaternion.LookRotation(initialAngle,Vector3.back);

		//Physics Rotation
		//Quaternion startRbRot = Quaternion.LookRotation(initialAngle,Vector3.back);
		//rb.MoveRotation(startRbRot);
		

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
				Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position, Vector3.back);
				transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				//rb.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
				//rb.MovePosition((Vector3)transform.position + (transform.forward * speed * 5f* Time.deltaTime));
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
		if (path != null && pathView){
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
