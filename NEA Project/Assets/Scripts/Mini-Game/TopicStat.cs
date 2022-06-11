using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TopicStatistics
{
    [SerializeField] int topicID;
    [SerializeField] int score;
    [SerializeField] int numOfIncorrectAnswers;
    [SerializeField] int numOfCorrectAnswers;

    public int TopicID { get { return topicID; } set { topicID = value; } }
    public int Score { get { return score; } set { score = value; } }
    public int NumOfIncorrectAnswers { get { return numOfIncorrectAnswers; } set { numOfIncorrectAnswers = value; } }
    public int NumOfCorrectAnswers { get { return numOfCorrectAnswers; } set { numOfCorrectAnswers = value; } }

    public TopicStatistics(int topicIndex)
    {
        topicID = topicIndex;
    }

    public void ModifyScore(float value)
    {
        score += (int)value;

        // Makes sure the score cannot be negative
        Mathf.Clamp(score, 0, Mathf.Infinity);
    }

    public void IncrementIncorrectAnswers()
    {
        numOfIncorrectAnswers++;
    }

    public void IncrementCorrectAnswers()
    {
        numOfCorrectAnswers++;
    }
}
