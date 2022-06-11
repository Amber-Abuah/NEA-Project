using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimePeriodValidater : MonoBehaviour
{
    [SerializeField] TMP_InputField timePeriodInput;
    [SerializeField] int timePeriod;

    [SerializeField] LineGraphGenerator lineGraphGenerator;

    public int TimePeriod { get { return timePeriod;}}

    public void CheckTimePeriod()
    {
        bool wasSuccessfulCast = false;

        // Determine whether the time period date text entered by the user can be parsed into an integer
        wasSuccessfulCast = int.TryParse(timePeriodInput.text, out timePeriod);

        // If the text can be successfully parsed
        if (wasSuccessfulCast)
        {
            if (timePeriod <= 90 && timePeriod >= 1)
                lineGraphGenerator.PositionVertices(timePeriod); // Apply the time period change to the line graph
            else
                timePeriod = 90;
        }
    }
}
