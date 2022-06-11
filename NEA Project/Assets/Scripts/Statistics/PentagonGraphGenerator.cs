using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PentagonGraphGenerator : MonoBehaviour
{
    PlayerInformation playerInformation;
    [SerializeField] List<DatabaseActivityData> data;

    DatabaseManipulator databaseManipulator;

    [SerializeField] PolygonGenerator polygonGenerator;

    [SerializeField] LineGraphVertex [] lineGraphVertices;

    [SerializeField] Transform originTrans;
    [SerializeField] Transform [] endTrans;

    Vector3[] vertexPoints;

    [SerializeField] GameObject pentagon;

    [SerializeField] TextMeshProUGUI [] feedbackText;
    [SerializeField] TMP_Dropdown topicDropdown;

    DatabaseActivityData[] latestTopicData;

    bool firstTopicVertexPlaced, secondTopicVertexPlaced, thirdTopicVertexPlaced, fourthTopicVertexPlaced, fifthTopicVertexPlaced;

    [TextArea]
    [SerializeField] string perfectMessage, averageMessage, badMessage;

    void PlacePentagonGraphVertex(int vertexIndex, int currentDataIndex)
    {
        // Find the position of the current vertex, based on the percentage of correct answers
        Vector3 position = (endTrans[vertexIndex].position - originTrans.position) * data[currentDataIndex].CorrectAnswerPercentage + lineGraphVertices[vertexIndex].transform.position;

        // Assign the position to the vertex
        lineGraphVertices[vertexIndex].transform.position = position;

        // Hold the position of this vertex in the vertex points array
        vertexPoints[vertexIndex] = position;

        // Change its tooltip tip to the current piece of data
        lineGraphVertices[vertexIndex].ChangeDateAndAnswers(data[currentDataIndex].Date, data[currentDataIndex].CorrectAnswers + "/" + (data[currentDataIndex].CorrectAnswers + data[currentDataIndex].IncorrectAnswers) + " questions correct");
    }

    void WriteTopicMessage(string perfectTitle, string averageTitle, string badTitle, int vertexIndex, int currentDataIndex)
    {
        // Display a message based on whether on the percentage of correct answers.
        feedbackText[vertexIndex].text = data[currentDataIndex].CorrectAnswerPercentage > 0.8f ? perfectTitle + perfectMessage : data[currentDataIndex].CorrectAnswerPercentage > 0.5f ? averageTitle + averageMessage : badTitle + badMessage;
    }

    void Start()
    {
        latestTopicData = new DatabaseActivityData[5];
        databaseManipulator = new DatabaseManipulator();
        playerInformation = PlayerInformation.Instance;
        vertexPoints = new Vector3[5];

        // Retrieve data from the database on activity
        data = databaseManipulator.PerformSelectPlayerActivity(playerInformation == null ? 1 : playerInformation.PlayerID);

        // Place vertices for all five topics
        PlaceVertices();

        // Generate the overlying pentagon based on the position of the vertices just placed
        polygonGenerator.GeneratePolygon(vertexPoints);
    }

    void PlaceVertices()
    {
        // Loop through all data retrieved from database
        for (int i = 0; i < data.Count; i++)
        {
            // If the a vertex representing the current topic has not been placed, place a vertex and display a message based on percentage of correct answers.
            if (data[i].TopicID == 1 && !firstTopicVertexPlaced)
            {
                PlacePentagonGraphVertex(0, i);
                WriteTopicMessage("Logic Gate Master : ", "Logical Amateur : ", "Logic Novice : ", 0, i);

                firstTopicVertexPlaced = true;
            }
            else if (data[i].TopicID == 2 && !secondTopicVertexPlaced)
            {
                PlacePentagonGraphVertex(1, i);
                WriteTopicMessage("Binary God : ", "Binary Amateur : ", "Binary Novice : ", 1, i);

                secondTopicVertexPlaced = true;
            }
            else if (data[i].TopicID == 3 && !thirdTopicVertexPlaced)
            {
                PlacePentagonGraphVertex(2, i);
                WriteTopicMessage("Boolean Master : ", "Boolean Amateur : ", "Boolean Novice : ", 2, i);

                thirdTopicVertexPlaced = true;
            }
            else if (data[i].TopicID == 4 && !fourthTopicVertexPlaced)
            {
                PlacePentagonGraphVertex(3, i);
                WriteTopicMessage("Sound Master : ", "Sound Amateur : ", "Sound Novice : ", 3, i);

                fourthTopicVertexPlaced = true;
            }
            else if (data[i].TopicID == 5 && !fifthTopicVertexPlaced)
            {
                PlacePentagonGraphVertex(4, i);
                WriteTopicMessage("DR Master : ", "DR Amateur : ", "DR Novice : ", 4, i);

                fifthTopicVertexPlaced = true;
            }
        }
    }

    public void ChangePentagonVisibilty()
    {
        if(topicDropdown.captionText.text == "All Areas")
            pentagon.SetActive(true);
        else
            pentagon.SetActive(false);
    }
}
