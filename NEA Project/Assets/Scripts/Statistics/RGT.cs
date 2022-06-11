using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGT : MonoBehaviour
{
    [SerializeField] float [] x, y;

    private void Start()
    {
        List<Vector2> list;

        list = new List<Vector2>();

        for (int i = 0; i < x.Length; i++)
        {
            list.Add(new Vector2(x[i], y[i]));
        }

        Calculate(list);
    }

    public float Calculate(List<Vector2> xyValues)
    {
        float sumOfX = 0;
        float sumOfY = 0;

        float sumOfXSquared = 0;
        float sumOfYSquared = 0;

        float SumOfXAndY = 0;

        int n = xyValues.Count;

        for (int i = 0; i < n; i++)
        {
            sumOfX += xyValues[i].x;
            sumOfXSquared += (xyValues[i].x * xyValues[i].x);

            sumOfY += xyValues[i].y;
            sumOfYSquared += (xyValues[i].y * xyValues[i].y);

            SumOfXAndY += xyValues[i].x * xyValues[i].y;

            Debug.Log(i.ToString() + " ---------------------------------------");
            Debug.Log("sumOfX : " + sumOfX);
            Debug.Log("sumOfXSquared : " + sumOfXSquared);
            Debug.Log("sumOfY : " + sumOfY);
            Debug.Log("sumOfYSquared : " + sumOfYSquared);
            Debug.Log("SumOfXAndY : " + SumOfXAndY);
            Debug.Log("---------------------------------------");

        }

        // Use formula for calculating coefficent of correlation
        float numerator = (n * SumOfXAndY) - (sumOfX * sumOfY);
        float denominator = ((n * sumOfXSquared) - (sumOfX * sumOfX)) * ((n * sumOfYSquared) - (sumOfY * sumOfY));

        Debug.Log("numerator " + numerator);
        Debug.Log("denominator " + denominator);

        float result = numerator / Mathf.Sqrt(denominator);
        Debug.Log("result " + result);
        return result;
    }
}