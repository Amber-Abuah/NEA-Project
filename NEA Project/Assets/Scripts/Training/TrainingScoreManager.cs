using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainingScoreManager : MonoBehaviour
{
    [SerializeField] int overallScore;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TrainingPlayerMovement playerMovement;
    [SerializeField] TrainingQuestionManager questionManager;

    [SerializeField] int currentTopicIndex;

    // Final score is current score plus the distance the player has travelled
    public int FinalScore { get { return overallScore + (int)playerMovement.DistanceTravelled; } }

    void Start()
    {
        scoreText.text = "0";

    }

    public void ModifyScore(float value)
    {
        // Keeps track of the overall, combined score for all topics
        overallScore += (int)value;

        // Makes sure the score cannot be negative
        Mathf.Clamp(overallScore, 0, Mathf.Infinity);

        // Display the score to the UI
        scoreText.text = overallScore.ToString();
    }

}
