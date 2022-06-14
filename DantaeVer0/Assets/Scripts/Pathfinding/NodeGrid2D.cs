using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid2D : MonoBehaviour
{
     public Transform player;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public LayerMask unwalkableMask;
    Node2D[,] grid;
    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start() {
        nodeDiameter = nodeRadius*2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node2D[gridSizeX,gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.up * gridSizeY/2;

        for (int x=0; x < gridSizeX; x++)
        {
            for (int y=0; y<gridSizeY; y++)
            {
                //! 2D Box Collider is Needed for Unwalkable Detection with Astar
                Vector2 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint,nodeRadius,unwalkableMask));
                grid[x,y] = new Node2D(walkable,worldPoint,x,y);
            }
        }
            
    }

    public Node2D NodeFromWorldPoint(Vector2 worldPosition){
        float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y/2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
        return grid[x,y];
    }

    public List<Node2D> GetNeighbours(Node2D node){
        List<Node2D> neighbours = new List<Node2D>();

        for (int x=-1; x<= 1; x++){
            for (int y=-1; y<= 1; y++){
                if(x == 0 && y== 0)
                    continue;
                
                int checkX = node.gridX +x;
                int checkY = node.gridY +y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX,checkY]);
                }
            }
        }
        return neighbours;
    }

    public List<Node2D> path;
    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position,new Vector2(gridWorldSize.x,gridWorldSize.y));

        if (grid != null)
        {
            Node2D playerNode = NodeFromWorldPoint(player.position);

            foreach(Node2D n in grid)
            {
                Gizmos.color = (n.walkable)?Color.white:Color.red;
                if (path != null)
                    if (path.Contains(n))
                        Gizmos.color = Color.black;
                if (playerNode ==n){
                    Gizmos.color = Color.cyan;
                }
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-0.1f));
            }
        }
    }   
}
