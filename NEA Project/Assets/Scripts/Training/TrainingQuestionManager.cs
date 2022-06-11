using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class TrainingQuestionManager : MonoBehaviour
{
    QuestionCollection questionCollection;

    Question currentQuestion;
    int currentQuestionListIndex;
    int currentQuestionIndex;
    int correctAnswerIndex;

    int numOfQuestions;
    int currentQuestionAmount;

    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] TextMeshProUGUI topicText;
    [SerializeField] TextMeshProUGUI[] answersText;
    [SerializeField] TextMeshProUGUI resultAnswerText;

    [SerializeField] int correctAnswerScore;
    [SerializeField] float questionInterval;
    [SerializeField] TrainingScoreManager scoreManager;
    [SerializeField] TrainingPlayerMovement playerMovement;
    [SerializeField] EndOfGameManager endOfGameManager;
    [SerializeField] TrainingPathSpawner pathSpawner;

    [SerializeField] TrainingAnswerAnimator answerAnimator;

    [SerializeField] float time;
    int chosenAnswerIndex;
    bool questionVisible;

    public string CurrentTopic { get { return questionCollection.TopicNames[currentQuestionListIndex]; } }
    public List<int> TopicIndexes { get { return questionCollection.TopicIndexes; } }

    // Singleton instance
    static TrainingQuestionManager instance;
    public static TrainingQuestionManager Instance { get { return instance; } }

    void Awake()
    {
        instance = this;

        // Sets all indexes to -1
        chosenAnswerIndex = -1;
        currentQuestionIndex = -1;
        currentQuestionListIndex = 0;

        ResetAnswers();
    }


    public void DisplayQuestion(string question, string answer1, string answer2, string answer3, int correctAnswer)
    {
        questionVisible = true;
        pathSpawner.IsQuestionPhase = true;

        // Display question to UI
        questionText.text = question;
        resultAnswerText.text = "";

        ResetAnswers();

        // Puts the answer in one of the three textboxes, based on the random number generated
        answersText[0].text = answer1;
        answersText[1].text = answer2;
        answersText[2].text = answer3;

        correctAnswerIndex = correctAnswer;
    }

    public void InputAnswer(int answerIndex)
    {
        chosenAnswerIndex = answerIndex;

        // If answer was correct
        if (chosenAnswerIndex == correctAnswerIndex)
        {
            // Add 100 to the current score
            scoreManager.ModifyScore(correctAnswerScore);

            Debug.Log("Question correct!");

            // Increase the correct answers for the current topic

            answerAnimator.CorrectAnswer();

            // resultAnswerText.text = "Correct answer!";
        }
        else
        {
            Debug.Log("Question wrong!");

            // Increase the incorrect answers for the current topic
            // Display that the user has answered incorrectly to the UI
            // resultAnswerText.text = "Incorrect answer!";

            answerAnimator.IncorrectAnswer();
        }

        chosenAnswerIndex = -1;
        time = 0;

        HideQuestion();
    }

    void HideQuestion()
    {
        // Set all text to invisible
        pathSpawner.IsQuestionPhase = false;

        questionVisible = false;

        questionText.text = "";
        ResetAnswers();
    }

    void ResetAnswers()
    {
        // Loop through all answer textbboxes and set them to empty strings
        for (int i = 0; i < answersText.Length; i++)
        {
            answersText[i].text = "";
        }
    }

    void Update()
    {
    }
}
