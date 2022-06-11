using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class LineGraphGenerator : MonoBehaviour
{
    [SerializeField] LineGraphVertex[] vertices;
    [SerializeField] int timePeriod;
    [SerializeField] Image vertexContainer;
    [SerializeField] Vector2 containerSize;

    [SerializeField] GameObject vertexPrefab;
    [SerializeField] Transform vertexParentTransform;
    [SerializeField] TMP_Dropdown topicDropdown;
    [SerializeField] TMP_Dropdown activityDataToShowDropdown;
    [SerializeField] GameObject timePeriodUiParent;
    [SerializeField] TextMeshProUGUI emptyDataText;
    [SerializeField] TextMeshProUGUI regressionLineText;

    [SerializeField] string currentTopic;
    [SerializeField] string currentActivityData;

    TimePeriodValidater timePeriodValidater;

    DatabaseManipulator databaseManipulator;

    RegressionLineCalculator regressionLineCalculator;

    [SerializeField] List<DatabaseActivityData> data;

    [SerializeField] LineRendererManager lineRendererManager;

    List<DateTime> activityDataDates;

    PlayerInformation playerInformation;

    const int maxAmountOfVertices = 90;

    public void ChangeTopic()
    {
        // Gets current topic from dropdown textbox text
        currentTopic = topicDropdown.captionText.text;

        if (GetCurrentTopicID() == 0) // If the 'All Areas' option is selected, hide the second dropdown menu and the time period UI
        {
            activityDataToShowDropdown.gameObject.SetActive(false);
            timePeriodUiParent.SetActive(false);
        }
        else
        {
            activityDataToShowDropdown.gameObject.SetActive(true);
            timePeriodUiParent.SetActive(true);
        }

        // If a time period has not been entered by the user yet, show data for the maximum time period.
        // If a time period has been entered, show data within the chosen time period.
        PositionVertices(timePeriodValidater.TimePeriod == 0 ? maxAmountOfVertices : timePeriodValidater.TimePeriod);

        if (GetCurrentTopicID() == 0)
            regressionLineText.text = "";
    }

    public void ChangeDataToShow()
    {
        // Find the current data type to be showed from the dropdown textbox text
        currentActivityData = activityDataToShowDropdown.captionText.text;

        // Reposition the vertices based on time period.
        PositionVertices(timePeriodValidater.TimePeriod == 0 ? maxAmountOfVertices : timePeriodValidater.TimePeriod);
    }

    void Start()
    {
        playerInformation = PlayerInformation.Instance;

        activityDataDates = new List<DateTime>();
        emptyDataText.gameObject.SetActive(false);

        databaseManipulator = new DatabaseManipulator();
        timePeriodValidater = GetComponent<TimePeriodValidater>();
        regressionLineCalculator = new RegressionLineCalculator();

        // Retrieve information from database about player activities
        data = databaseManipulator.PerformSelectPlayerActivity(playerInformation == null? 1 : playerInformation.PlayerID);

        for (int i = 0; i < data.Count; i++)
        {
            Debug.Log(data[i].Score + " achieved on " + data[i].Date);

            // Convert the string date to a DateTime object
            activityDataDates.Add(Convert.ToDateTime(data[i].Date));
            Debug.Log("Time span " + (DateTime.Now - activityDataDates[i]).Days + " days");
        }
        
        // Testing
        for (int i = 0; i < activityDataDates.Count; i++)
        {
            Debug.Log("Dates for dt = " + activityDataDates[i].ToLongDateString());
        }

        currentTopic = topicDropdown.captionText.text;
        currentActivityData = activityDataToShowDropdown.captionText.text;

        timePeriodUiParent.SetActive(false);
        
        // Get height and width of the container that the vertices will be put in
        var rectTransform = vertexContainer.GetComponent<RectTransform>();
        containerSize.x = rectTransform.rect.width;
        containerSize.y = rectTransform.rect.height;

        // Instantiate all vertices and place them in the correct positon
        CreateVertices();
        PositionVertices(maxAmountOfVertices);

        regressionLineText.text = "";
    }

    void CreateVertices()
    {
        vertices = new LineGraphVertex[maxAmountOfVertices];

        // Create all 90 vertices
        for (int i = 0; i < maxAmountOfVertices; i++)
        {
            vertices[i] = Instantiate(vertexPrefab).GetComponent<LineGraphVertex>();
            vertices[i].name = "Vertex " + i.ToString();
            vertices[i].transform.SetParent(vertexParentTransform);
        }
    }

    void HideAllVertices()
    {
        // Loop through all vertices and set them as invisible
        for (int i = 0; i < maxAmountOfVertices; i++)
        {
            vertices[i].gameObject.SetActive(false);
        }
    }

    public void PositionVertices(float maxAmount)
    {
        bool firstPoint = true;
        bool foundActivityDataForCurrentDate;
        bool noDataToShow = true;

        List<Vector2> xyValues;

        LineRenderer currentLineRenderer = null;

        xyValues = new List<Vector2>();

        // Hide all current lines and vertices
        HideAllVertices();
        lineRendererManager.HideAllLines();
        
        // Find the maximum score from the selected time range 
        int maxScore = DetermineMaxScore((int) maxAmount);

        maxScore = 1400;

        // If a maximum score has been found, then data is available to plot
        if (maxScore != 0)
        {
            // Loop through all vertices
            for (int i = 0; i < maxAmount; i++)
            {
                foundActivityDataForCurrentDate = false;

                // Find the current date this vertex is referring to

                DateTime currentDate;

                if (PlayerInformation.Instance == null)
                    currentDate = (DateTime.Now.Subtract(new TimeSpan((int)maxAmount - (i + 1), 0, 0, 0)).Subtract(TimeSpan.FromDays(63)));
                else if(PlayerInformation.Instance.PlayerID == 1)
                    currentDate = (DateTime.Now.Subtract(new TimeSpan((int)maxAmount - (i + 1), 0, 0, 0)).Subtract(TimeSpan.FromDays(63)));
                else
                    currentDate = (DateTime.Now.Subtract(new TimeSpan((int)maxAmount - (i + 1), 0, 0, 0)));

                // Loop through all data collected from the database
                for (int j = 0; j < activityDataDates.Count; j++)
                {
                    // If a piece of data is found representing the current date
                    if (activityDataDates[j].Date == currentDate.Date && data[j].TopicID == GetCurrentTopicID() && !foundActivityDataForCurrentDate)
                    {
                        foundActivityDataForCurrentDate = true;
                        noDataToShow = false;

                        // Make the current vertex visible
                        vertices[i].gameObject.SetActive(true);

                        //Debug.Log("Data found for " + data[j].Score + "for " + currentDate.ToLongDateString() + "on node " + i.ToString());

                        // If the current activity data type is Answer data
                        if (GetCurrentActivityDataType() == 1)
                        {
                            // Change the current vertex's tooltip to the current date and and the number of correct answers.
                            vertices[i].ChangeDateAndAnswers(currentDate.ToString("yyyy-MM-dd"), data[j].CorrectAnswers + "/" + data[j].TotalAmountOfAnswers + " answers correct");

                            // Position this vertex based on the correct answer percentage
                            vertices[i].transform.localPosition = new Vector3(containerSize.x * (i / maxAmount), containerSize.y * (data[j].CorrectAnswerPercentage), 0);
                        }
                        else
                        {
                            // Change the current vertex's tooltip to the current date and score.
                            vertices[i].ChangeDateAndScore(currentDate.ToString("yyyy-MM-dd"), data[j].Score);

                            // Position this vertex in relation to the highest score
                            vertices[i].transform.localPosition = new Vector3(containerSize.x * (i / maxAmount), containerSize.y * (data[j].Score / (float)maxScore), 0);

                        }

                        xyValues.Add(new Vector2(i, data[j].CorrectAnswerPercentage));

                        // If this is the first point being plotted
                        if (firstPoint)
                        {
                            // Retrieve the next available line renderer
                            currentLineRenderer = lineRendererManager.RetrievePooledObject();

                            // If a line renderer was retrieved
                            if (currentLineRenderer != null)
                            {
                                // Set its starting position to the current vertex
                                currentLineRenderer.SetPosition(0, vertices[i].transform.position);
                                firstPoint = false;
                            }
                        }
                        else
                        {
                            if (currentLineRenderer != null)
                            {
                                // Set the vertex ending position to the current vertex
                                currentLineRenderer.SetPosition(1, vertices[i].transform.position);

                                // Retrieve the next line renderer and set its starting postion to the current vertex also
                                currentLineRenderer = lineRendererManager.RetrievePooledObject();
                                currentLineRenderer.SetPosition(0, vertices[i].transform.position);
                            }
                        }
                    }
                }
                //Debug.Log("Date " + currentDate.ToLongDateString());
            }
        }

        void ChangeRegressionLineText(float regressionCoefficient)
        {
            if(regressionCoefficient < -0.5)
                regressionLineText.text = "There is quite a sharp overall decrease for this subject. You are getting increasingly worse at this subject area over time.";
            else if (regressionCoefficient < 0)
                regressionLineText.text = "There is a slight overall decrease. You are getting slightly worse at this subject over time.";
            else if (regressionCoefficient < 0.5)
                regressionLineText.text = "There is a slight overall increase. You are getting a little better at this subject over time.";
            else
                regressionLineText.text = "There is a big overall increase. You are getting a lot better at this subject area over time!";
        }

        // Hide the last line as it has no end position
        if (currentLineRenderer != null)
            currentLineRenderer.gameObject.SetActive(false);

        // If no data has been found and no points plotted, then show a textbox saying no data was found
        if (noDataToShow && topicDropdown.captionText.text != "All Areas")
        {
            emptyDataText.gameObject.SetActive(true);
            regressionLineText.text = "";
        }
        else
        {
            emptyDataText.gameObject.SetActive(false);
            ChangeRegressionLineText(regressionLineCalculator.Calculate(xyValues));
        }

        Debug.Log(regressionLineCalculator.Calculate(xyValues));
       // ChangeRegressionLineText(regressionLineCalculator.Calculate(xyValues));
    }

    int DetermineMaxScore(int timePeriod)
    {
        // Find max score in time period
        int maxScore = 0;

        // Determine the earliest date in the given time period
        DateTime minDate = DateTime.Now.Subtract(new TimeSpan(timePeriod, 0, 0, 0));

        // Loop through data collected from database and find the highest score
        for (int i = 0; i < data.Count; i++)
        {
            // If the date for this score is in the time range
            if(activityDataDates[i] >= minDate)
            {
                // If the score is bigger than the max score
                if (data[i].Score > maxScore)
                {
                    if (data[i].TopicID == GetCurrentTopicID())
                        maxScore = data[i].Score; // Change the max score to the current score
                }
            }
        }

        Debug.Log("The max score for this time period is " + maxScore);
        return maxScore;
    }

    int GetCurrentTopicID()
    {
        switch (currentTopic)
        {
            case "Logic Gates":
                return 1;
            case "Binary":
                return 2;
            case "Boolean Algebra":
                return 3;
            case "Sound":
                return 4;
            case "Data Representation":
                return 5;
            default:
                return 0;
        }
    }

    int GetCurrentActivityDataType()
    {
        switch (currentActivityData)
        {
            case "Answers":
                return 1;
            case "Score":
                return 2;
            default:
                return 0;
        }
    }
}
