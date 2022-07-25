using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Unity.Collections;

public class UnityListPriorityQueue : MonoBehaviour
{
    static int size = 0;
    static int maxsize = 0;
    private const int FRONT = 1;

    // Start is called before the first frame update
    void Start()
    {
        var sw = new Stopwatch();
        sw.Start();

        //* H is the index
        //*values is the fCost
        NativeArray<int> H = new NativeArray<int>(1, Allocator.Temp);

        SetSize(0);
        SetMaxsize(49);
        
        int[] values = new int[]{0,0,5,3,17,10,84,19,6,22,9};
        
        H[0] = 0;
        insert(1, H, values);
        insert(2, H, values);
        insert(3, H, values);
        insert(4, H, values);
        insert(5, H, values);
        insert(6, H, values);
        insert(7, H, values);
        insert(8, H, values);
        insert(9, H, values);
        insert(10, H, values);
        
        //* Have size H[] = size values
        //*Keep in mind first index is invalid

        printTree(values,H);

       for (int i = 0; i < 10; i++) {
        // Node with maximum priority
        UnityEngine.Debug.Log("Node with minimum priority : " +
                    remove(H, values));
        }

        /*
        UnityEngine.Debug.Log("Removed value : " +
                    remove(H, values));

        UnityEngine.Debug.Log("Node with Get Min priority : " +
                    getMin(values,H));

        
        UnityEngine.Debug.Log("Value with get min : " +
                    getMinValue(H));
        */

        /*
        // Node with maximum priority
        UnityEngine.Debug.Log("Node with Getmaximum priority : " +
                    getMin(H));
        
        remove(H, values);

        // Node with maximum priority
        UnityEngine.Debug.Log("Node with GetMaxmaximum priority : " +
                    getMin(H));
        
        remove(H, values);

        // Node with maximum priority
        UnityEngine.Debug.Log("Node with GetMaxmaximum priority : " +
                    getMin(H));

        remove(H, values);

        insert(3, H, values);

        // Node with maximum priority
        UnityEngine.Debug.Log("Node with GetMaxmaximum priority : " +
                    remove(H, values));


        // Node with maximum priority
        UnityEngine.Debug.Log("Node with GetMaxmaximum priority : " +
                    getMin(H));
        */

        H.Dispose();
        
        
        sw.Stop();
        UnityEngine.Debug.Log("elapsed time: " + sw.Elapsed.TotalMilliseconds + " milliseconds");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

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
    private void swap(int fpos, int spos, NativeArray<int> H, int[] values)
    {
 
        int tmp;
        tmp = H[fpos];
 
        H[fpos] = H[spos];
        H[spos] = tmp;
    }
    
    // Method 6
    // To heapify the node at pos
   private void minHeapify(int pos, NativeArray<int> H, int[] values)
   {     
     if(!isLeaf(pos)){
        
       //swap with the minimum of the two children
       int swapPos = GetFCost(values,H[leftChild(pos)]) < GetFCost(values,H[rightChild(pos)]) ? leftChild(pos):rightChild(pos);
        
       if(GetFCost(values,H[pos]) > GetFCost(values,H[leftChild(pos)])  || GetFCost(values,H[pos]) > GetFCost(values,H[rightChild(pos)])){
         swap(pos,swapPos, H, values);
         minHeapify(swapPos, H, values);
       }
        
     }      
   }
    
    // Method 7
    // To insert a node into the heap
    public void insert(int element, NativeArray<int> H, int[] values)
    {
 
        if (size >= maxsize) {
            return;
        }
        H[++size] = element;
        int current = size;
 
        while (GetFCost(values,H[current]) < GetFCost(values,H[parent(current)])) {
            swap(current, parent(current), H, values);
            current = parent(current);
        }
    }
    
    // Method 8
    // To print the contents of the heap
    public void print(int[] values, NativeArray<int> H)
    {
        for (int i = 0; i < H.Length; i++) {
 
            // Printing the parent and both childrens
            UnityEngine.Debug.Log(" LISTING : " + H[i]);
        }
    }

    public void printTree(int[] values,NativeArray<int> H)
    {
        for (int i = 1; i <= size / 2; i++) {
 
            // Printing the parent and both childrens
            UnityEngine.Debug.Log(
                " PARENT : " + GetFCost(values,H[i])
                + " LEFT CHILD : " + GetFCost(values,H[2 * i])
                + " RIGHT CHILD :" + GetFCost(values,H[2 * i + 1]));

        }
    }
    
     // Method 9
    // To remove and return the minimum
    // element from the heap
    public int remove(NativeArray<int> H, int[] values)
    {
        int popped = H[FRONT];
        H[FRONT] = H[size--];
        minHeapify(FRONT, H, values);
 
        return GetFCost(values,popped);
    }

    static int GetFCost(int[] value, int index){
        int fCost = value[index];
        return fCost;
    }
}
