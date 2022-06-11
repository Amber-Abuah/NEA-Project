using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSorrt : MonoBehaviour
{
    int maxsize = 7;
    [SerializeField] int[] items;


    private void Start()
    {
        //items = new int[maxsize];

       // items[0] = 10;
        //items[1] = 100;
       // items[2] = 76;
       // items[3] = 4;
       // items[4] = 51;
       // items[5] = 34;
        //items[6] = 1;

        MergeSort(items, 1);

    }

    // Sort the items in the queue in order of their priority
    void MergeSort(int[] mergeList, int callNum)
    {
        int mid;
        int i = 0, j = 0, k = 0;
        bool isEven;
        int[] leftHalf, rightHalf;

        if (mergeList.Length > 1)
        {

            /*Debug.Log("----------------------------------");
            Debug.Log("callNum " + callNum + "param = " );

            for (int l = 0; l < mergeList.Length; l++)
            {
                Debug.Log(mergeList[l]);
            }

            Debug.Log("----------------------------------");*/

            mid = (mergeList.Length / 2);
            isEven = (mergeList.Length % 2 == 0) ? true : false;

            Debug.Log("mid " + mid);

            leftHalf = new int[mid];
            rightHalf = new int[isEven? mid : mid + 1];

            Array.Copy(mergeList, 0, leftHalf, 0, mid);
            Array.Copy(mergeList, mid, rightHalf, 0, isEven ? mid : mid + 1);

          /*  Debug.Log("---------------------------------------------");
            Debug.Log("Left half");
            for (int o = 0; o < leftHalf.Length; o++)
            {
                Debug.Log("leftHalf " + o.ToString()+ ": " + leftHalf[o]);
            }
            Debug.Log("Right half");

            for (int o = 0; o < rightHalf.Length; o++)
            {
                Debug.Log("rightHalf " + o.ToString() + ": " + rightHalf[o]);
            }

            Debug.Log("---------------------------------------------");*/

            MergeSort(leftHalf, callNum + 1);
            MergeSort(rightHalf, callNum + 2);

            while (i < leftHalf.Length && j < rightHalf.Length)
            {
                if (leftHalf[i] < rightHalf[j])
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

                Debug.Log("---------------------------------------------");

                Debug.Log("i " + i);
                Debug.Log("j " + j);
                Debug.Log("k " + k);

                for (int l = 0; l < mergeList.Length; l++)
                {
                    Debug.Log("merge list " + l + ": " + mergeList[l]);
                }

                Debug.Log("---------------------------------------------");

            }

            while (i < leftHalf.Length)
            {
                mergeList[k] = leftHalf[i];
                i++;
                k++;



                Debug.Log("---------------------------------------------");

                Debug.Log("i " + i);
                Debug.Log("j " + j);
                Debug.Log("k " + k);

                for (int l = 0; l < mergeList.Length; l++)
                {
                    Debug.Log("merge list " + l + ": " + mergeList[l]);
                }

                Debug.Log("---------------------------------------------");
            }

            while (j < rightHalf.Length)
            {
                mergeList[k] = rightHalf[j];
                j++;
                k++;

                Debug.Log("---------------------------------------------");

                Debug.Log("i " + i);
                Debug.Log("j " + j);
                Debug.Log("k " + k);

                for (int l = 0; l < mergeList.Length; l++)
                {
                    Debug.Log("merge list " + l + ": " + mergeList[l]);
                }

                Debug.Log("---------------------------------------------");
            }
        }
    }
}