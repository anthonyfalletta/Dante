using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Jobs;
using Unity.Burst;
using System.Diagnostics;
using System;

public class PathfindingHeap : MonoBehaviour
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    
    private void Start() {
        var sw = new Stopwatch();
        sw.Start();

        /*
        int findPathJobCount = 5;
        NativeArray<JobHandle> jobHandleArray = new NativeArray<JobHandle>(findPathJobCount, Allocator.TempJob);
        FindPathJob findPathJob = new FindPathJob{
                startPosition = new int2(0,0),
                endPosition = new int2(2,3)
            }; 

        for(int i=0; i<5; i++){   
            jobHandleArray[i] = findPathJob.Schedule();
        }

        JobHandle.CompleteAll(jobHandleArray);
        jobHandleArray.Dispose();
        */

        FindPathJob findPathJob = new FindPathJob{startPosition=new int2(0, 0), endPosition=new int2(50,50)};
        findPathJob.Run();

        sw.Stop();
        UnityEngine.Debug.Log("elapsed time: " + sw.Elapsed.TotalMilliseconds + " milliseconds");
    }

    //![BurstCompile]
    private struct FindPathJob : IJob{

        public int2 startPosition;
        public int2 endPosition;
        int currentItemCount;

        public void Execute(){
        
        //Setup Grid
        int2 gridSize = new int2(100,100);

        NativeArray<PathNode> pathNodeArray = new NativeArray<PathNode>(gridSize.x * gridSize.y, Allocator.Temp);
        //Try Moving outside gloabally
        NativeArray<PathNode>items = new NativeArray<PathNode>(gridSize.x*gridSize.y,Allocator.Temp);

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

        //NativeList<int> openList = new NativeList<int>(Allocator.Temp);
        NativeList<int> closedList = new NativeList<int>(Allocator.Temp);

        //openList.Add(startNode.index);

        //HeapMaxSize(gridSize.x*gridSize.y);
        Add(startNode, items);

        while (Count > 0){
            //TODO Heap Optimize this code
            PathNode currentNode = RemoveFirst(items);
            int currentNodeIndex = currentNode.index;


            closedList.Add(currentNodeIndex);

            if (currentNodeIndex == endNodeIndex){
                //Reach Destination
                break;
            }

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

                    if (!Contains(neighbourNode, items)){
                        Add(neighbourNode, items);
                    }
                }
            }
        }

        PathNode endNode = pathNodeArray[endNodeIndex];

        if (endNode.cameFromNodeIndex == -1){
            //Didn't find path
        }else{
            //Found Path
            NativeList<int2> path = CalculatePath(pathNodeArray, endNode);

            //!Does not work in burst fyi
            
            foreach(int2 pathPosition in path){
                UnityEngine.Debug.Log(pathPosition);
            }
            

            path.Dispose();
        }

        pathNodeArray.Dispose();
        neighbourOffsetArray.Dispose();
        items.Dispose();
        //openList.Dispose();
        closedList.Dispose(); 
        }

        /*
        private void HeapMaxSize(int maxHeapSize){
            items = new PathNode[maxHeapSize];
        } 
        */

        private void Add(PathNode item, NativeArray<PathNode> items){
            item.HeapIndex = currentItemCount;
            items[currentItemCount] = item;
            SortUp(item, items);
            currentItemCount++;
        }

        private PathNode RemoveFirst(NativeArray<PathNode> items){
            PathNode firstItem = items[0];
            currentItemCount--;
            items[0] = items[currentItemCount];
            items[0].SetHeapIndex(0);
            SortDown(items[0], items);
            return firstItem;
        }

        private void UpdateItem(PathNode item, NativeArray<PathNode> items){
            SortUp(item, items);
        }

        public int Count{
            get{
                return currentItemCount;
            }
        }

        private bool Contains(PathNode item, NativeArray<PathNode> items){
            return Equals(items[item.HeapIndex], item);
        }

        private void SortDown(PathNode item, NativeArray<PathNode> items){
            while(true){
                int childIndexLeft = item.HeapIndex * 2+1;
                int childIndexRight = item.HeapIndex * 2+2;
                int swapIndex = 0;

                if(childIndexLeft < currentItemCount){
                    swapIndex = childIndexLeft;
                    if(childIndexRight < currentItemCount){
                        if(items[childIndexLeft].fCost > items[childIndexRight].fCost){
                            swapIndex = childIndexRight;
                        }
                    }
                    if (item.fCost > items[swapIndex].fCost){
                        Swap(item,items[swapIndex], items);
                    }
                    else{
                        return;
                    }
                }
                else{
                    return;
                }
            }
        }
        private void SortUp(PathNode item, NativeArray<PathNode> items){
            int parentIndex = (item.HeapIndex-1)/2;

            while(true){
                PathNode parentItem = items[parentIndex];
                if (item.fCost < parentItem.fCost){
                    Swap(item, parentItem, items);
                }
                else{
                    break;
                }

                parentIndex = (item.HeapIndex-1)/2;
            }
        }

        private void Swap(PathNode itemA, PathNode itemB, NativeArray<PathNode> items){
            items[itemA.HeapIndex] = itemB;
            items[itemB.HeapIndex] = itemA;
            int itemAIndex = itemA.HeapIndex;
            itemA.HeapIndex = itemB.HeapIndex;
            itemB.HeapIndex = itemAIndex;
        }

        //Functions for A* Algorithm
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
                UnityEngine.Debug.Log("Lowest FCost - i: " + i);
                PathNode testPathNode = pathNodeArray[openList[i]];
                if (testPathNode.fCost < lowestCostPathNode.fCost){
                    lowestCostPathNode = testPathNode;
                    UnityEngine.Debug.Log("Lowest FCost - OpenList i: " + i);
                }
            }
            return lowestCostPathNode.index;
            }



        
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

            public int HeapIndex;

            public void CalculateFCost(){
                fCost = gCost + hCost;
            }

            public void SetIsWalkable(bool isWalkable){
                this.isWalkable = isWalkable;
            }

            public int GetHeapIndex(){
                return HeapIndex;
            }

            public int SetHeapIndex(int _heapIndex){
                return HeapIndex = _heapIndex;
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






