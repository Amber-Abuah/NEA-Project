using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int overallScore;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] QuestionManager questionManager;

    [SerializeField] List<TopicStatistics> topicStats;
    [SerializeField] int currentTopicIndex;

    // Final score is current score plus the distance the player has travelled
    public int FinalScore { get {return overallScore + (int)playerMovement.DistanceTravelled; } }
    public List<TopicStatistics> TopicStats { get {return topicStats; } }
    
    void Start()
    {
        scoreText.text = "0";

        topicStats = new List<TopicStatistics>();

        // Add topicStat objects to the topicStats list with the correct revision topic index
        for (int i = 0; i < questionManager.TopicIndexes.Count; i++)
        {
            topicStats.Add(new TopicStatistics(questionManager.TopicIndexes[i]));
        }
    }

    public void ModifyScore(float value)
    {
        // Modify the score for the current topic
        topicStats[currentTopicIndex].ModifyScore(value);

        // Keeps track of the overall, combined score for all topics
        overallScore += (int) value;

        // Makes sure the score cannot be negative
        Mathf.Clamp(overallScore, 0, Mathf.Infinity);

        // Display the score to the UI
        scoreText.text = overallScore.ToString();
    }

    public void IncrementTopicIncorrectAnswers()
    {
        topicStats[currentTopicIndex].IncrementIncorrectAnswers();
    }

    public void IncrementTopicCorrectAnswers()
    {
        topicStats[currentTopicIndex].IncrementCorrectAnswers();
    }

    public void NextTopic()
    {
        currentTopicIndex++;
    }
}
