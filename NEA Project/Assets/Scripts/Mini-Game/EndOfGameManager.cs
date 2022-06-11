using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfGameManager : MonoBehaviour
{
    [SerializeField] GameObject endOfGameScreen, gameOverScreen;
    [SerializeField] TextMeshProUGUI timeTakenText, scoreText;
    [SerializeField] Timer timer;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] RevisionItemManager revisionItemManager;

    [SerializeField] DatabaseManipulator databaseManipulator;

    void Start()
    {
        databaseManipulator = new DatabaseManipulator();
    }

    public void EndGame()
    {
        // Show the end of game UI
        endOfGameScreen.SetActive(true);

        // Stop timer and player movement
        timer.StopTimer();
        playerMovement.StopPlayerMovement();

        // Display results to UI
        timeTakenText.text += timer.TimeElapsed.ToString("00") + " seconds";
        scoreText.text += scoreManager.FinalScore;

        // Insert play session statisitics into database
        for (int i = 0; i < scoreManager.TopicStats.Count; i++)
        {
            int playerID;

            if (PlayerInformation.Instance == null)
                playerID = 1;
            else
                playerID = PlayerInformation.Instance.PlayerID;

            databaseManipulator.PerformInsertIntoPlayerActivityCommand(playerID, scoreManager.TopicStats[i].Score, scoreManager.TopicStats[i].NumOfCorrectAnswers, scoreManager.TopicStats[i].NumOfIncorrectAnswers, scoreManager.TopicStats[i].TopicID);
        }

        // Write the links picked up to the database
        revisionItemManager.WriteLinksPickedUpToDatabase();
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);

        // Stop timer and player movement
        timer.StopTimer();
        playerMovement.StopPlayerMovement();
    }
}
