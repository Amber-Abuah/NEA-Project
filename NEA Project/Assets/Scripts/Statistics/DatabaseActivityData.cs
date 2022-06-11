using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DatabaseActivityData
{
    [SerializeField] int score;
    [SerializeField] int correctAnswers;
    [SerializeField] int incorrectAnswers;
    [SerializeField] string date;
    [SerializeField] int topicID;

    public int Score { get { return score; } }
    public int CorrectAnswers { get { return correctAnswers; } }
    public int IncorrectAnswers { get { return incorrectAnswers; } }
    public string Date { get { return date; } }
    public int TopicID { get { return topicID; } }

    public float CorrectAnswerPercentage { get { return (float)correctAnswers / (float)TotalAmountOfAnswers; } }
    public float TotalAmountOfAnswers { get { return correctAnswers + incorrectAnswers; } }

    public DatabaseActivityData(int scoreAmount, int correctAnswerAmount, int incorrectAnswerAmount, string datadate, int revisionTopicID)
    {
        score = scoreAmount;
        correctAnswers = correctAnswerAmount;
        incorrectAnswers = incorrectAnswerAmount;
        date = datadate;
        topicID = revisionTopicID;
    }
}
