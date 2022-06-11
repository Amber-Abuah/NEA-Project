using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStatsToDatabase : MonoBehaviour
{
    DatabaseManipulator databaseManipulator;

    int finalScore;
    int numOfQuestions;
    int numOfIncorrectAnswers;
    int numOfCorrectAnswers;

    void WriteToDatabase()
    {
        databaseManipulator = new DatabaseManipulator();
    }

}
