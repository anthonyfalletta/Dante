using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using diag = System.Diagnostics;
using System;

public class PathfindingPersonal : MonoBehaviour
{
 	
	//PathRequestManager requestManager;
	NodeGrid grid;
	public Transform target;
	public bool simplifyPath;
	Unit unit;
	
	void Awake() {
		//requestManager = GetComponent<PathRequestManager>();
		grid = GetComponent<NodeGrid>();
		unit = this.GetComponent<Unit>();
		
	}
	
	
	public void FindPath(PathRequest request, Action<PathResult> callback) {
		
		diag.Stopwatch sw = new diag.Stopwatch();
		sw.Start();
		
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;
		
		Node startNode = grid.NodeFromWorldPoint(request.pathStart);
		Node targetNode = grid.NodeFromWorldPoint(request.pathEnd);
		startNode.parent = startNode;
		
		
		if (startNode.walkable && targetNode.walkable) {
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);
			
			while (openSet.Count > 0) {
				Node currentNode = openSet.RemoveFirst();
				closedSet.Add(currentNode);
				
				if (currentNode == targetNode) {
					sw.Stop();
					print ("Path found: " + sw.ElapsedMilliseconds + " ms");
					pathSuccess = true;
					break;
				}
				
				foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
					if (!neighbour.walkable || closedSet.Contains(neighbour)) {
						continue;
					}
					
					int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour) + neighbour.movementPenalty;
					if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
						neighbour.gCost = newMovementCostToNeighbour;
						neighbour.hCost = GetDistance(neighbour, targetNode);
						neighbour.parent = currentNode;
						
						if (!openSet.Contains(neighbour))
							openSet.Add(neighbour);
						else 
							openSet.UpdateItem(neighbour);
					}
				}
			}
		}
		if (pathSuccess) {
			waypoints = RetracePath(startNode,targetNode, request.pathEnd);
			pathSuccess = waypoints.Length > 0;
		}
		else if (!pathSuccess){
			Debug.Log("Path not found :(");
		}
		callback(new PathResult(waypoints, pathSuccess, request.callback));
		
	}

	Vector3[] RetracePath(Node startNode, Node endNode, Vector3 targetVector) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;
		
		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}

		//List<Vector3[]> waypointsTemp = new List<Vector3[]>();
		if (simplifyPath){
			Vector3[] waypoints = SimplifyPath(path, targetVector);
			Array.Reverse(waypoints);
			return waypoints;
		}
		else
		{
			Vector3[] waypoints = ReturnPath(path, targetVector);
			Array.Reverse(waypoints);
			return waypoints;
		}
		//Add target position to waypoints at first or 0 position to go all the way to player
		
		//Array.Reverse(waypoints);
		//return waypoints;
		
	}
	
	Vector3[] SimplifyPath(List<Node> path, Vector3 targetPosition) {
		List<Vector3> waypoints = new List<Vector3>();
		Vector2 directionOld = Vector2.zero;
		
		//Add player postion to path
		//TODO: Setup better structure in scripts to get player positon
		//waypoints.Add(target.position);
		//waypoints.Add(unit.target);
		waypoints.Add(targetPosition);

		for (int i = 1; i < path.Count; i ++) {
			Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX,path[i-1].gridY - path[i].gridY);
			if (directionNew != directionOld) {
				waypoints.Add(path[i].worldPosition);
			}
			directionOld = directionNew;
		}	
		return waypoints.ToArray();
	}

	Vector3[] ReturnPath(List<Node> path, Vector3 targetPosition) {
		List<Vector3> waypoints = new List<Vector3>();
		Vector2 directionOld = Vector2.zero;
		
		//Add player postion to path
		//TODO: Setup better structure in scripts to get player positon
		//waypoints.Add(target.position);
		//if (unit.target != null)
		waypoints.Add(targetPosition);

		for (int i = 1; i < path.Count; i ++) {
			waypoints.Add(path[i].worldPosition);
		}	
		return waypoints.ToArray();
	}
	
	int GetDistance(Node nodeA, Node nodeB) {
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
		
		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
	}
	
	
}
