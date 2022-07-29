using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Jobs;
using Unity.Burst;
using Unity.Entities;

public class Pathfinding : ComponentSystem
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, DynamicBuffer<PathPosition> pathPositionBuffer, ref PathfindingParams pathfindingParams) => {
            Debug.Log("FindPath!");
            FindPathJob findPathJob = new FindPathJob{
                startPosition = pathfindingParams.startPosition,
                endPosition = pathfindingParams.endPosition,
                pathPositionBuffer = pathPositionBuffer,
                entity = entity,
                pathFollowComponentDataFromEntity = GetComponentDataFromEntity<PathFollow>()
            };
            findPathJob.Run();

            PostUpdateCommands.RemoveComponent<PathfindingParams>(entity);
        });
        
    }

    [BurstCompile]
    private struct FindPathJob : IJob{

        public int2 startPosition;
        public int2 endPosition;

        public Entity entity;
        public ComponentDataFromEntity<PathFollow> pathFollowComponentDataFromEntity;
        public DynamicBuffer<PathPosition> pathPositionBuffer;

        public void Execute(){
            //Setup Grid
        int2 gridSize = new int2(100,100);

        NativeArray<PathNode> pathNodeArray = new NativeArray<PathNode>(gridSize.x * gridSize.y, Allocator.Temp);

        for (int x=0; x<gridSize.x; x++){
            for (int y=0; y < gridSize.y; y++){
                PathNode pathNode = new PathNode();
                pathNode.x = x;
                pathNode.y = y;
                pathNode.index = CalculateIndex(x,y,gridSize.x);

                pathNode.gCost = int.MaxValue;
                pathNode.hCost = CalculateDistanceCost(new int2(x,y), endPosition);
                pathNode.CalculateFCost();

                pathNode.isWalkable = true;
                pathNode.cameFromNodeIndex = -1;

                pathNodeArray[pathNode.index] = pathNode;
            }
        }
        PathNode walkalbePathNode = pathNodeArray[CalculateIndex(1,0,gridSize.x)];
        walkalbePathNode.SetIsWalkable(false);
        pathNodeArray[CalculateIndex(1,0,gridSize.x)] = walkalbePathNode;

        walkalbePathNode = pathNodeArray[CalculateIndex(1,1,gridSize.x)];
        walkalbePathNode.SetIsWalkable(false);
        pathNodeArray[CalculateIndex(1,1,gridSize.x)] = walkalbePathNode;
        

        //int2[] does not operate in burst compiler
        /*
        NativeArray<int2> neighbourOffsetArray = new NativeArray<int2>(new int2[]{
            new int2(-1,0),//Left
            new int2(+1,0),//Right
            new int2(0, +1),//Up
            new int2(0, -1),//Down
            new int2(-1,-1),//Left Down
            new int2(-1,+1),//Left Up
            new int2(+1, -1),//Right Down
            new int2(+1, +1),//RIght Up
        }, Allocator.Temp);
        */

        NativeArray<int2> neighbourOffsetArray = new NativeArray<int2>(8, Allocator.Temp);
        neighbourOffsetArray[0] = new int2(-1,0);//Left
        neighbourOffsetArray[1] =  new int2(+1,0);//Right   
        neighbourOffsetArray[2] = new int2(0,+1);//Up
        neighbourOffsetArray[3] =  new int2(0,-1);//Down   
        neighbourOffsetArray[4] = new int2(-1,-1);//Left Down
        neighbourOffsetArray[5] =  new int2(-1,+1);//Left Up   
         neighbourOffsetArray[6] = new int2(+1,-1);//Right Down
        neighbourOffsetArray[7] =  new int2(+1,+1);//Right Up      

        //Calculation End Node Index
        int endNodeIndex = CalculateIndex(endPosition.x, endPosition.y, gridSize.x);

        //Add Start Node to Array
        PathNode startNode = pathNodeArray[CalculateIndex(startPosition.x, startPosition.y, gridSize.x)];
        startNode.gCost = 0;
        startNode.CalculateFCost();
        pathNodeArray[startNode.index] = startNode;

        NativeList<int> openList = new NativeList<int>(Allocator.Temp);
        NativeList<int> closedList = new NativeList<int>(Allocator.Temp);

        openList.Add(startNode.index);

        while (openList.Length > 0){
            //TODO Heap Optimize this code
            //* In GetLowestCostFNodeIndex have reference to Min. Heap where will go through tree top to bottom down, left to right (faster than array search)
            int currentNodeIndex = GetLowestCostFNodeIndex(openList, pathNodeArray);
            PathNode currentNode = pathNodeArray[currentNodeIndex];
            if (currentNodeIndex == endNodeIndex){
                //Reach Destination
                break;
            }

            //Remove current node from Open List
            //* Could this be simplified to if openList[currentNodeIndex] == currentNode or do Max. Heap search as faster
            for (int i =0; i < openList.Length; i++){
                if (openList[i] == currentNodeIndex){
                    openList.RemoveAtSwapBack(i);   
                    break;
                }
            }

            closedList.Add(currentNodeIndex);

            //Cycle through neighbours of current node
            for (int i=0; i < neighbourOffsetArray.Length; i++){
                int2 neighbourOffset = neighbourOffsetArray[i];
                int2 neighbourPosition = new int2(currentNode.x + neighbourOffset.x, currentNode.y + neighbourOffset.y);

                if(!IsPostionInsideGrid(neighbourPosition, gridSize)){
                    //Neighbour not valid postion
                    continue;
                }

                int neighbourNodeIndex = CalculateIndex(neighbourPosition.x, neighbourPosition.y, gridSize.x);

                if (closedList.Contains(neighbourNodeIndex)){
                    //Already search this node
                    continue;
                }

                PathNode neighbourNode = pathNodeArray[neighbourNodeIndex];
                if (!neighbourNode.isWalkable){
                    //Not Walkable
                    continue;
                }

                int2 currentNodePosition = new int2(currentNode.x, currentNode.y);
                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNodePosition, neighbourPosition);
                if (tentativeGCost < neighbourNode.gCost){
                    neighbourNode.cameFromNodeIndex = currentNodeIndex;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.CalculateFCost();
                    pathNodeArray[neighbourNodeIndex] = neighbourNode;

                    if (!openList.Contains(neighbourNode.index)){
                        openList.Add(neighbourNode.index);
                    }
                }
            }
        }

        pathPositionBuffer.Clear();

        PathNode endNode = pathNodeArray[endNodeIndex];

        if (endNode.cameFromNodeIndex == -1){
            //Didn't find path
            pathFollowComponentDataFromEntity[entity] = new PathFollow{pathIndex = -1};
        }else{
            //Found Path
            CalculatePath(pathNodeArray, endNode, pathPositionBuffer);
            pathFollowComponentDataFromEntity[entity] = new PathFollow{pathIndex = pathPositionBuffer.Length-1};
        }

        pathNodeArray.Dispose();
        neighbourOffsetArray.Dispose();
        openList.Dispose();
        closedList.Dispose();
        }



        private void CalculatePath (NativeArray<PathNode> pathNodeArray, PathNode endNode, DynamicBuffer<PathPosition> pathPositionBuffer){
            if (endNode.cameFromNodeIndex == -1){
                //Didn't find path
            }else{
                //Found Path
                //Retrace Path (Path is inverted)
                pathPositionBuffer.Add(new PathPosition{position = new int2(endNode.x, endNode.y)});

                PathNode currentNode = endNode;
                while (currentNode.cameFromNodeIndex != -1){
                    PathNode cameFromNode = pathNodeArray[currentNode.cameFromNodeIndex];
                    pathPositionBuffer.Add(new PathPosition{position = new int2(cameFromNode.x, cameFromNode.y)});
                    currentNode = cameFromNode;
                }
            }
        }
        private NativeList<int2> CalculatePath (NativeArray<PathNode> pathNodeArray, PathNode endNode){
        if (endNode.cameFromNodeIndex == -1){
            //Didn't find path
            return new NativeList<int2>(Allocator.Temp);
        }else{
            //Found Path
            //Retrace Path (Path is inverted)
            NativeList<int2> path = new NativeList<int2>(Allocator.Temp);
            path.Add(new int2(endNode.x, endNode.y));

            PathNode currentNode = endNode;
            while (currentNode.cameFromNodeIndex != -1){
                PathNode cameFromNode = pathNodeArray[currentNode.cameFromNodeIndex];
                path.Add(new int2(cameFromNode.x, cameFromNode.y));
                currentNode = cameFromNode;
            }

            return path;
        }
    }

    private bool IsPostionInsideGrid(int2 gridPosition, int2 gridSize){
        return 
        gridPosition.x >= 0 && 
        gridPosition.y >= 0 && 
        gridPosition.x < gridSize.x &&
        gridPosition.y < gridSize.y;
    }

    private int CalculateIndex(int x, int y, int gridWidth){
        return x + y * gridWidth;
    }

    private int CalculateDistanceCost(int2 aPosition, int2 bPosition){
        int xDistance = math.abs(aPosition.x - bPosition.x);
        int yDistance = math.abs(aPosition.y - bPosition.y);
        int remaining = math.abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * math.min(xDistance,yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private int GetLowestCostFNodeIndex(NativeList<int> openList, NativeArray<PathNode> pathNodeArray){
        PathNode lowestCostPathNode = pathNodeArray[openList[0]];
        for (int i = 1; i< openList.Length; i++){
            PathNode testPathNode = pathNodeArray[openList[i]];
            if (testPathNode.fCost < lowestCostPathNode.fCost){
                lowestCostPathNode = testPathNode;
            }
        }
        return lowestCostPathNode.index;
        }



    //Node struct for reference with A* algorithm
    private struct PathNode{
        public int x;
        public int y;

        public int index;

        public int gCost;
        public int hCost;
        public int fCost;

        public bool isWalkable;
        public int cameFromNodeIndex;

        public void CalculateFCost(){
            fCost = gCost + hCost;
        }

        public void SetIsWalkable(bool isWalkable){
            this.isWalkable = isWalkable;
        }

        public int HeapIndex{
            get{
                return HeapIndex;
            }
            set{
                HeapIndex = value;
            }
        }

        public int CompareTo(PathNode nodeToCompare){
            int compare = fCost.CompareTo(nodeToCompare.fCost);
            if(compare==0){
                compare = hCost.CompareTo(nodeToCompare.hCost);
            }
            return -compare;
        }
        }
    }
        
}


