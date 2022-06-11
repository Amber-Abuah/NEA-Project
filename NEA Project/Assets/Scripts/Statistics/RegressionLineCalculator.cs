using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegressionLineCalculator : MonoBehaviour
{
    [SerializeField] int[] x, y;

   public float Calculate(List<Vector2> xyValues)
    {
        float sumOfX = 0;
        float sumOfY = 0;

        float sumOfXSquared = 0;
        float sumOfYSquared = 0;

        float SumOfProductOfXAndY = 0;

        int n = xyValues.Count;

        for (int i = 0; i < n; i++)
        {
            sumOfX += xyValues[i].x;
            sumOfXSquared += (xyValues[i].x * xyValues[i].x);

            sumOfY += xyValues[i].y;
            sumOfYSquared += (xyValues[i].y * xyValues[i].y);

            SumOfProductOfXAndY += xyValues[i].x * xyValues[i].y;
        }
        
        // Use formula for calculating coefficent of correlation
        float numerator = (n * SumOfProductOfXAndY) - (sumOfX * sumOfY);
        float denominator = ((n * sumOfXSquared) - (sumOfX * sumOfX)) * ((n * sumOfYSquared) - (sumOfY * sumOfY));

        float result = numerator / Mathf.Sqrt(denominator);
        return result;
    }
}