using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class QueueHeap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sw = new Stopwatch();
        sw.Start();

        insert(45);
        insert(20);
        insert(14);
        insert(12);
        insert(31);
        insert(7);
        insert(11);
        insert(13);
        insert(7);
        
        int i = 0;

        
        // Node with maximum priority
        UnityEngine.Debug.Log("Node with Getmaximum priority : " +
                    getMax());

        // Node with maximum priority
        UnityEngine.Debug.Log("Node with Extractmaximum priority : " +
                    extractMax());
        
        // Node with maximum priority
        UnityEngine.Debug.Log("Node with GetMaxmaximum priority : " +
                    getMax());
        
        

        sw.Stop();
        UnityEngine.Debug.Log("elapsed time: " + sw.Elapsed.TotalMilliseconds + " milliseconds");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static int []H = new int[50];
    static int size = -1;
    
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
    
    // Function to shift up the
    // node in order to maintain
    // the heap property
    static void shiftUp(int i)
    {
        while (i > 0 &&
            H[parent(i)] < H[i])
        {
            
            // Swap parent and current node
            swap(parent(i), i);
        
            // Update i to parent of i
            i = parent(i);
        }
    }
    
    // Function to shift down the node in
    // order to maintain the heap property
    static void shiftDown(int i)
    {
        int maxIndex = i;
        
        // Left Child
        int l = leftChild(i);
        
        if (l <= size &&
            H[l] > H[maxIndex])
        {
            maxIndex = l;
        }
        
        // Right Child
        int r = rightChild(i);
        
        if (r <= size &&
            H[r] > H[maxIndex])
        {
            maxIndex = r;
        }
        
        // If i not same as maxIndex
        if (i != maxIndex)
        {
            swap(i, maxIndex);
            shiftDown(maxIndex);
        }
    }
    
    // Function to insert a
    // new element in
    // the Binary Heap
    static void insert(int p)
    {
        size = size + 1;
        H[size] = p;
        
        // Shift Up to maintain
        // heap property
        shiftUp(size);
    }
    
    // Function to extract
    // the element with
    // maximum priority
    static int extractMax()
    {
        int result = H[0];
        
        // Replace the value
        // at the root with
        // the last leaf
        H[0] = H[size];
        size = size - 1;
        
        // Shift down the replaced
        // element to maintain the
        // heap property
        shiftDown(0);
        return result;
    }
    
    // Function to change the priority
    // of an element
    static void changePriority(int i,
                            int p)
    {
        int oldp = H[i];
        H[i] = p;
        
        if (p > oldp)
        {
            shiftUp(i);
        }
        else
        {
            shiftDown(i);
        }
    }
    
    // Function to get value of
    // the current maximum element
    static int getMax()
    {
        return H[0];
    }
    
    // Function to remove the element
    // located at given index
    static void Remove(int i)
    {
        H[i] = getMax() + 1;
        
        // Shift the node to the root
        // of the heap
        shiftUp(i);
        
        // Extract the node
        extractMax();
    }
    
    static void swap(int i, int j)
    {
        int temp = H[i];
        H[i] = H[j];
        H[j] = temp;
    }
}
