using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Unity.Collections;

public class MinListPriorityQueueHeap : MonoBehaviour
{
    static int size = 0;
    static int maxsize = 0;

    // Start is called before the first frame update
    void Start()
    {
        var sw = new Stopwatch();
        sw.Start();

        
        NativeArray<int> H = new NativeArray<int>(50, Allocator.Temp);
        SetSize(-1);
        SetMaxsize(H.Length-1);

        
        insert(45, H);
        
        insert(20, H);
        insert(14, H);
        insert(12, H);
        insert(31, H);
        insert(7, H);
        insert(11, H);
        insert(13, H);
        insert(7, H);
        
        for (int i = 0; i < H.Length; i++) {
 
            // Printing the parent and both childrens
            UnityEngine.Debug.Log(" LISTING : " + H[i]);
        }
        
        // Node with maximum priority
        UnityEngine.Debug.Log("Node with Getmaximum priority : " +
                    getMin(H));

        remove(H);
        // Node with maximum priority
        UnityEngine.Debug.Log("Node with Getmaximum priority : " +
                    getMin(H));
        
        remove(H);

        // Node with maximum priority
        UnityEngine.Debug.Log("Node with GetMaxmaximum priority : " +
                    getMin(H));
        
        remove(H);

        // Node with maximum priority
        UnityEngine.Debug.Log("Node with GetMaxmaximum priority : " +
                    getMin(H));

        remove(H);

        insert(3, H);

        // Node with maximum priority
        UnityEngine.Debug.Log("Node with GetMaxmaximum priority : " +
                    remove(H));


        // Node with maximum priority
        UnityEngine.Debug.Log("Node with GetMaxmaximum priority : " +
                    getMin(H));
        
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
        return (i - 1) / 2;
    }
    
    // Function to return the index of the
    // left child of the given node
    static int leftChild(int i)
    {
        return ((2 * i) + 1);
    }
    
    // Function to return the index of the
    // right child of the given node
    static int rightChild(int i)
    {
        return ((2 * i) + 2);
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
    private void swap(int fpos, int spos, NativeArray<int> H)
    {
 
        int tmp;
        tmp = H[fpos];
 
        H[fpos] = H[spos];
        H[spos] = tmp;
    }
    
    // Method 6
    // To heapify the node at pos
   private void minHeapify(int pos, NativeArray<int> H)
   {     
     if(!isLeaf(pos)){
        
       //swap with the minimum of the two children
       int swapPos = H[leftChild(pos)]<H[rightChild(pos)]?leftChild(pos):rightChild(pos);
        
       if(H[pos]>H[leftChild(pos)] || H[pos]> H[rightChild(pos)]){
         swap(pos,swapPos, H);
         minHeapify(swapPos, H);
       }
        
     }      
   }
    
    // Method 7
    // To insert a node into the heap
    public void insert(int element, NativeArray<int> H)
    {
 
        if (size >= maxsize) {
            return;
        }
 
        H[++size] = element;
        int current = size;
 
        while (H[current] < H[parent(current)]) {
            swap(current, parent(current), H);
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
    
     // Method 9
    // To remove and return the minimum
    // element from the heap
    public int remove(NativeArray<int> H)
    {
 
        int popped = H[0];
        H[0] = H[size--];
        minHeapify(0, H);
 
        return popped;
    }
    
    static int getMin(NativeArray<int> H)
    {
        return H[0];
    }
}
