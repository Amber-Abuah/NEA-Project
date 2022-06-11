using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PriorityQueue <T> where T : Item
{
    T[] queue;
    int rearPointer;
    int maxSize;

    public bool IsFull { get { return (rearPointer + 1) == maxSize ? true : false; } }
    public bool IsEmpty { get { return rearPointer == -1 ? true : false;}}
    public int Size { get { return rearPointer + 1; } }

    public PriorityQueue(int numOfElements)
    {
        queue = new T[numOfElements];
        maxSize = numOfElements;
        rearPointer = -1;
    }
     
    public void Enqueue(T item, bool sort = true)
    {
        // If queue is not full, add item and increment rear pointer
        if (!IsFull)
        {
            rearPointer++;
            queue[rearPointer] = item;
           // Debug.Log("Added item " + item.ItemName + "!");

            if (sort)
            {
                // Make a duplicate array. However the size will set to the amount of items, instead of the maximum size.
                T[] queue2 = new T[Size];
                Array.Copy(queue, 0, queue2, 0, Size);

                // Sort the items in the duplicate queue
                MergeSort(queue2);

                // Copy the elements of the duplicate array over to the orignal array
                for (int i = 0; i < Size; i++)
                {
                    queue[i] = queue2[i];
                }
            }
        }
    }

    // Sort the items in the queue in order of their priority
    void MergeSort(T[] mergeList)
    {
        int mid;
        int i = 0, j = 0, k = 0;
        bool isEven;
        T[] leftHalf, rightHalf;

        if (mergeList.Length > 1)
        {
            // Find the middle point of the array
            mid = (mergeList.Length / 2);
            isEven = (mergeList.Length % 2 == 0) ? true : false;

            leftHalf = new T[mid];
            rightHalf = new T[isEven ? mid : mid + 1];

            Array.Copy(mergeList, 0, leftHalf, 0, mid);
            Array.Copy(mergeList, mid, rightHalf, 0, isEven ? mid : mid + 1);

            MergeSort(leftHalf);
            MergeSort(rightHalf);

            while (i < leftHalf.Length && j < rightHalf.Length)
            {
                if (leftHalf[i].ItemPriority < rightHalf[j].ItemPriority)
                {
                    mergeList[k] = leftHalf[i];
                    i++;
                }
                else
                {
                    mergeList[k] = rightHalf[j];
                    j++;
                }
                k++;
            }

            while (i < leftHalf.Length)
            {
                mergeList[k] = leftHalf[i];
                i++;
                k++;
            }

            while (j < rightHalf.Length)
            {
                mergeList[k] = rightHalf[j];
                j++;
                k++;
            }
        }
    }

    // Shifts all items one to the left when an item is dequeued
    void ShiftElements()
    {
        for (int i = 0; i < rearPointer + 1; i++)
        {
            queue[i] = queue[i + 1];
        }
    }

    public void ViewQueue()
    {
        for (int i = 0; i < rearPointer + 1; i++)
        {
            Debug.Log("Item " + (i + 1) + " is " + queue[i].ItemName + " with priority " + queue[i].ItemPriority);
        }

        Debug.Log("There are " + (rearPointer + 1) + " items in the queue." );

        if (IsEmpty) Debug.Log("It is empty."); else Debug.Log("It is not empty.");
        if (IsFull) Debug.Log("It is full."); else Debug.Log("It is not full.");
    }

    public T Dequeue()
    {
        T removedItem;

        if (IsEmpty)
        {
           // Debug.Log("Queue is empty!");
            return default(T); // Return null
        }
        else
        {
            removedItem = queue[0];
            rearPointer--;
            ShiftElements(); // Shift all elements to the left, after an item has been removed from front of queue

           // Debug.Log("Removing " + item.ItemName + " with priority " + item.ItemPriority +  " from queue!");
            return removedItem;
        }
    }

    // Returns item at index 0
    public T Peek()
    {
        if (!IsEmpty)
            return queue[0];
        else
            return default(T);
    }

    // Returns item at chosen index
    public T PeekItemAtIndex(int index)
    {
        if (index > rearPointer)
            return default(T);
        else
            return queue[index];
    }
}
