using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class MinPriorityQueueHeap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sw = new Stopwatch();
        sw.Start();

        H[0] = 0;

        insert(5);
        insert(3);
        insert(17);
        insert(10);
        insert(84);
        insert(0);
        insert(19);
        insert(6);
        insert(22);
        insert(9);
        
    
        printTree();

        /*
        for (int i = 0; i < H.Length; i++) {
 
            // Printing the parent and both childrens
            UnityEngine.Debug.Log(" LISTING : " + H[i]);
        }
        */
        
        for (int i = 0; i < 10; i++) {
        // Node with maximum priority
        UnityEngine.Debug.Log("Node with minimum priority : " +
                    remove());
        }
        

        sw.Stop();
        UnityEngine.Debug.Log("elapsed time: " + sw.Elapsed.TotalMilliseconds + " milliseconds");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static int []H = new int[50];
    static int size = 0;
    static int maxsize = 49;
    private const int FRONT = 1;

    // Function to return the index of the
    // parent node of a given node
    static int parent(int i)
    {
        return (i / 2);
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
    private void swap(int fpos, int spos)
    {
 
        int tmp;
        tmp = H[fpos];
 
        H[fpos] = H[spos];
        H[spos] = tmp;
    }
    
    // Method 6
    // To heapify the node at pos
   private void minHeapify(int pos)
   {     
     if(!isLeaf(pos)){
        
       //swap with the minimum of the two children
       int swapPos = H[leftChild(pos)]<H[rightChild(pos)]?leftChild(pos):rightChild(pos);
        
       if(H[pos]>H[leftChild(pos)] || H[pos]> H[rightChild(pos)]){
         swap(pos,swapPos);
         minHeapify(swapPos);
       }
        
     }      
   }
    
    // Method 7
    // To insert a node into the heap
    public void insert(int element)
    {
 
        if (size >= maxsize) {
            return;
        }
 
        H[++size] = element;
        int current = size;
 
        while (H[current] < H[parent(current)]) {
            swap(current, parent(current));
            current = parent(current);
        }
    }
    
    // Method 8
    // To print the contents of the heap
    public void printTree()
    {
        for (int i = 1; i <= size / 2; i++) {
 
            // Printing the parent and both childrens
            UnityEngine.Debug.Log(
                " PARENT : " + H[i]
                + " LEFT CHILD : " + H[2 * i]
                + " RIGHT CHILD :" + H[2 * i + 1]);
        }
    }
    
     // Method 9
    // To remove and return the minimum
    // element from the heap
    public int remove()
    {
 
        int popped = H[FRONT];
        H[FRONT] = H[size--];
        minHeapify(FRONT);
 
        return popped;
    }
    
}
