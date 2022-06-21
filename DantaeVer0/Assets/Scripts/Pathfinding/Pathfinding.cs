using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.InputSystem;
using System;

public class Pathfinding : MonoBehaviour
{
    //! Double Check Path Logic path[i] addition and subtraction have some weird stuff going on

    //public Transform seeker, target;
    PathRequestManager requestManager;
    NodeGrid grid;
    private void Awake() {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<NodeGrid>();
    }

    /*
    private void Update() {
        if (Keyboard.current[Key.Space].wasPressedThisFrame)
        {
            FindPath(seeker.position, target.position);
        }
    }
    */

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);
        startNode.parent = startNode;


        if (startNode.walkable && targetNode.walkable)
        {
                Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
                HashSet<Node> closedSet = new HashSet<Node>();
                openSet.Add(startNode);

                while (openSet.Count > 0){
                    Node currentNode = openSet.RemoveFirst();
                    //Node currentNode = openSet[0];
                    /*
                    for (int i =1; i < openSet.Count; i++)
                    {
                        if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                        {
                            currentNode = openSet[i];
                        }
                    }
                    openSet.Remove(currentNode);
                    */
                    closedSet.Add(currentNode);

                    if (currentNode == targetNode)
                    {
                        sw.Stop();
                        print("Path found: " + sw.ElapsedTicks + " ticks");
                        pathSuccess = true;
                        //RetracePath(startNode,targetNode);
                        break;
                        //return;
                    }

                    //Adding Neighbours
                    AddNeighboursToPath(currentNode, openSet, closedSet, startNode, targetNode);
                    
                }
        }
            yield return null;
            
            RetraceAndSendFoundPath(waypoints, pathSuccess, startNode, targetNode);
    }


    private void RetraceAndSendFoundPath(Vector3[] waypoints, bool pathSuccess, Node startNode, Node targetNode){
        if(pathSuccess)
            {
                waypoints = RetracePath(startNode,targetNode);
            }
            requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }



    private void AddNeighboursToPath(Node currentNode, Heap<Node> openSet, HashSet<Node> closedSet, Node startNode, Node targetNode)
    {
        foreach (Node neighbor in grid.GetNeighbours(currentNode))
                    {
                        if (!neighbor.walkable || closedSet.Contains(neighbor))
                        {
                            continue;
                        }

                        int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode,neighbor) + neighbor.movementPenalty;
                        if (newMovementCostToNeighbour < neighbor.gCost || !openSet.Contains(neighbor)){
                            neighbor.gCost = newMovementCostToNeighbour;
                            neighbor.hCost = GetDistance(neighbor,targetNode);
                            neighbor.parent = currentNode;

                            if (!openSet.Contains(neighbor))
                                openSet.Add(neighbor);
                            else
                                openSet.UpdateItem(neighbor);
                        }
                    }
    }

    //void RetracePath(Node startNode, Node endNode)
    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;

        //grid.path = path;
    }

    //! Check to see if Vector2 versus Vector3 makes difference
    Vector3[] SimplifyPath(List<Node> path){
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i=1; i < path.Count; i++){
            Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX, path[i-1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i].worldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX > distY)
            return 14*distY + 10 * (distX-distY);
        return 14*distX + 10 * (distY-distX);
    }
}
