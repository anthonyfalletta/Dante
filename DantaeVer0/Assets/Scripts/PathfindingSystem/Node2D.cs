using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node2D
{
   public bool walkable;
   public Vector2 worldPosition;
   public int gCost;
   public int hCost;
   public int gridX;
   public int gridY;
   public Node2D parent;

   public Node2D(bool _walkable, Vector2 _worldPos, int _gridX, int _gridY){
       walkable = _walkable;
       worldPosition = _worldPos;
       gridX = _gridX;
       gridY = _gridY;
   }

   public int fCost {
       get{
           return gCost + hCost;
       }
   }
}
