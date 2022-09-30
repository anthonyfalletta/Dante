using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding2D : MonoBehaviour
{
    public Transform seeker, target;
    NodeGrid2D grid;
    private void Awake() {
        grid = GetComponent<NodeGrid2D>();
    }

    private void Update() {
        FindPath(seeker.position, target.position);
    }

    void FindPath(Vector2 startPos, Vector2 targetPos)
    {
        Node2D startNode = grid.NodeFromWorldPoint(startPos);
        Node2D targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node2D> openSet = new List<Node2D>();
        HashSet<Node2D> closedSet = new HashSet<Node2D>();

        openSet.Add(startNode);

        while (openSet.Count > 0){
            Node2D currentNode = openSet[0];
            for (int i =1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode,targetNode);
                return;
            }

            foreach (Node2D neighbors in grid.GetNeighbours(currentNode))
            {
                if (!neighbors.walkable || closedSet.Contains(neighbors))
                {
                    continue;
                }
                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode,neighbors);
                if (newMovementCostToNeighbour < neighbors.gCost || !openSet.Contains(neighbors)){
                    neighbors.gCost = newMovementCostToNeighbour;
                    neighbors.hCost = GetDistance(neighbors,targetNode);
                    neighbors.parent = currentNode;

                    if (!openSet.Contains(neighbors))
                        openSet.Add(neighbors);
                }
            }
        }
    }

    void RetracePath(Node2D startNode, Node2D endNode)
    {
        List<Node2D> path = new List<Node2D>();
        Node2D currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;
    }

    int GetDistance(Node2D nodeA, Node2D nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX < distY)
            return 14*distY + 10 * (distX-distY);
        return 13*distX + 10 * (distY-distX);
    }
}
