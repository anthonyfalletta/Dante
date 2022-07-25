using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Jobs;
using Unity.Burst;
using System.Diagnostics;
using System;

public class PathfindingChange : MonoBehaviour
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;
    static int size = 0;
    static int maxsize = 0;
    private const int FRONT = 1;
    
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

        FindPathJob findPathJob = new FindPathJob{startPosition=new int2(0, 0), endPosition=new int2(2,3)};
        findPathJob.Run();

        sw.Stop();
        UnityEngine.Debug.Log("elapsed time: " + sw.Elapsed.TotalMilliseconds + " milliseconds");
    }

    //![BurstCompile]
    private struct FindPathJob : IJob{

        public int2 startPosition;
        public int2 endPosition;

        public void Execute(){
            //Setup Grid
        int2 gridSize = new int2(4,4);

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
        
        walkalbePathNode = pathNodeArray[CalculateIndex(1,2,gridSize.x)];
        walkalbePathNode.SetIsWalkable(false);
        pathNodeArray[CalculateIndex(1,2,gridSize.x)] = walkalbePathNode;

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
        
        SetSize(0);
        SetMaxsize(gridSize.x*gridSize.y-1);
        openList.Add(0);

        //openList.Add(startNode.index);
        insert(startNode.index, openList, pathNodeArray);

        while (openList.Length > 0){
            

            //TODO Heap Optimize this code
            //*Will use min Priority Heap, will peek minimum and make sure lowere than top openList int for pathArray
            //* remove this openList min value  
            int currentNodeIndex = GetLowestCostFNodeIndex(openList, pathNodeArray);
            PathNode currentNode = pathNodeArray[currentNodeIndex];
        
            if (currentNodeIndex == endNodeIndex){
                //Reach Destination
                break;
            }

            
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
                        insert(neighbourNode.index, openList, pathNodeArray);
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
        openList.Dispose();
        closedList.Dispose();
        
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
        PathNode lowestCostPathNode = pathNodeArray[openList[FRONT]];
        PathNode testPathNode = pathNodeArray[getMin(openList)];
            if (testPathNode.fCost < lowestCostPathNode.fCost){
                lowestCostPathNode = testPathNode;
            }
    
        return lowestCostPathNode.index;
    }

        //Minimum Priority Heap Logic
        static void SetSize(int sizeValue){
            size = sizeValue;
        }

        static void SetMaxsize(int maxsizeValue){
            maxsize = maxsizeValue;
        }
        
        // Function to return the index of the
        // parent node of a given node
        static int parent(int i)
        {
            return (i) / 2;
        }
        
        // Function to return the index of the
        // left child of the given node
        static int leftChild(int i)
        {
            return ((2 * i));
        }
        
        // Function to return the index of the
        // right child of the given node
        static int rightChild(int i)
        {
            return ((2 * i) + 1);
        }
        
    // Method 4
        // Returning true if the passed
        // node is a leaf node
        private bool isLeaf(int pos)
        {
    
            if (pos > (size / 2)) {
                return true;
            }
    
            return false;
        }
        
        // Method 5
        // To swap two nodes of the heap
        private void swap(int fpos, int spos, NativeList<int> H)
        {
    
            int tmp;
            tmp = H[fpos];
    
            H[fpos] = H[spos];
            H[spos] = tmp;
        }
        
        // Method 6
        // To heapify the node at pos
    private void minHeapify(int pos, NativeList<int> H, NativeArray<PathNode> pathNodeArray)
    {     
        if(!isLeaf(pos)){
            
        //swap with the minimum of the two children
        int swapPos = GetFCost(pathNodeArray,H[leftChild(pos)]) < GetFCost(pathNodeArray,H[rightChild(pos)]) ? leftChild(pos):rightChild(pos);
            
        if(GetFCost(pathNodeArray,H[pos]) > GetFCost(pathNodeArray,H[leftChild(pos)])  || GetFCost(pathNodeArray,H[pos]) > GetFCost(pathNodeArray,H[rightChild(pos)])){
            swap(pos,swapPos, H);
            minHeapify(swapPos, H, pathNodeArray);
        }
            
        }      
    }
        
        // Method 7
        // To insert a node into the heap
        private void insert(int element, NativeList<int> H, NativeArray<PathNode> pathNodeArray)
        {
    
            if (size >= maxsize) {
                return;
            }
            H.Add(element);
            H[++size] = element;
            int current = size;
    
            while (GetFCost(pathNodeArray,H[current]) < GetFCost(pathNodeArray,H[parent(current)])) {
                swap(current, parent(current), H);
                current = parent(current);
            }
        }
        
        // Method 8
        // To print the contents of the heap
        public void print(NativeArray<int> values, NativeList<int> H)
        {
            for (int i = 0; i < H.Length; i++) {
    
                // Printing the parent and both childrens
                UnityEngine.Debug.Log(" LISTING : " + H[i]);
            }
        }

        private void printTree(NativeArray<PathNode> pathNodeArray,NativeList<int> H)
        {
            for (int i = 1; i <= size / 2; i++) {
    
                // Printing the parent and both childrens
                UnityEngine.Debug.Log(
                    " PARENT : " + GetFCost(pathNodeArray,H[i])
                    + " LEFT CHILD : " + GetFCost(pathNodeArray,H[2 * i])
                    + " RIGHT CHILD :" + GetFCost(pathNodeArray,H[2 * i + 1]));

            }
        }
        
        // Method 9
        // To remove and return the minimum
        // element from the heap
        private int remove(NativeList<int> H, NativeArray<PathNode> pathNodeArray)
        {
            int popped = H[FRONT];
            H[FRONT] = H[size--];

            minHeapify(FRONT, H, pathNodeArray);
    
            return popped;
        }

        public int getMin(NativeList<int> H)
        {
            return H[FRONT];
        }

        static int GetFCost(NativeArray<PathNode> pathNodeArray,int index){
            int fCost = pathNodeArray[index].fCost;
            return fCost;
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

        }
    }
        
}


