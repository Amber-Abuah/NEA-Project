using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
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
    [SerializeField] TextMeshProUGUI [] answersText;
    [SerializeField] TextMeshProUGUI resultAnswerText;

    [SerializeField] int correctAnswerScore;
    [SerializeField] float questionInterval;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] EndOfGameManager endOfGameManager;
    [SerializeField] Timer timer;
    [SerializeField] PathSpawner pathSpawner;

    [SerializeField] ResultAnswerAnimator answerAnimator;

    [SerializeField] float time;
    int chosenAnswerIndex;
    bool questionVisible;

    public string CurrentTopic { get { return questionCollection.TopicNames[currentQuestionListIndex]; } }
    public List<int> TopicIndexes { get { return questionCollection.TopicIndexes; } }

    // Singleton instance
    static QuestionManager instance;
    public static QuestionManager Instance { get { return instance; } }

    void Awake()
    {
        instance = this;

        // Sets all indexes to -1
        chosenAnswerIndex = -1;
        currentQuestionIndex =-1;
        currentQuestionListIndex = 0;

        // Converts JSON file to usable object
        ParseQuestions();

        NextQuestion();
    }

    void ParseQuestions()
    {
        float time = 0;

        questionCollection = new QuestionCollection();

        // Uses streamreader to read contents of JSON file
        using (StreamReader streamReader = new StreamReader(Application.dataPath + @"/StreamingAssets/Question.json"))
        {
            string json = streamReader.ReadToEnd();
            questionCollection = JsonUtility.FromJson<QuestionCollection>(json);
        }

        // Loops through the list of question objects to find sum of their times
        for (int i = 0; i < questionCollection.AllQuestions.Count; i++)
        {
            for (int j = 0; j < questionCollection.AllQuestions[i].Count; j++)
            {
                time += questionCollection.AllQuestions[i][j].Time;
                numOfQuestions++;
            }
        }

        timer.SetMaxTime(time);
    }

    // Change to next question
    void NextQuestion(bool displayQuestion = true)
    {
        currentQuestionAmount++;

        if (currentQuestionAmount > numOfQuestions)
        {
            Debug.Log("On question number " + currentQuestionIndex.ToString() + " from the " + currentQuestionListIndex + "th pack of questions.");
            Debug.Log("Congratulations! Game finished!");
            endOfGameManager.EndGame();
        }
        else
        {
            // If the index corresponds to the last question in the current topic, move onto the first question in the next topic
            if (currentQuestionIndex == questionCollection.AllQuestions[currentQuestionListIndex].Count - 1)
            {
                currentQuestionListIndex++;
                currentQuestionIndex = 0;

                scoreManager.NextTopic();
            }
            else // If not, move onto the next question in the same topic
                currentQuestionIndex++;

            // Displays the topic name to the UI
            topicText.text = questionCollection.TopicNames[currentQuestionListIndex];

            Debug.Log("On question number " + currentQuestionIndex.ToString() + " from the " + currentQuestionListIndex + "th pack of questions.");

            currentQuestion = questionCollection.AllQuestions[currentQuestionListIndex][currentQuestionIndex]; 

            if (displayQuestion)
                DisplayQuestion();
        }
    }

    void DisplayQuestion()
    {
        questionVisible = true;
        playerMovement.IsQuestionPhase = true;
        pathSpawner.IsQuestionPhase = true;

        // Display question to UI
        questionText.text = currentQuestion.QuestionToAsk;
        resultAnswerText.text = "";

        ResetAnswers();

        // Generates a random number position for the correct answer
        correctAnswerIndex = Random.Range(0, 3);

        // Puts the answer in one of the three textboxes, based on the random number generated
        answersText[correctAnswerIndex].text = currentQuestion.CorrectAnswer;

        // Slots the other two incorrect answers in the leftover textboxes
        bool firstWrongAnswerAdded = false;

        for (int i = 0; i < answersText.Length; i++)
        {
            if(answersText[i].text == "")
            {
                if (firstWrongAnswerAdded)
                    answersText[i].text = currentQuestion.WrongAnswer2;
                else
                {
                    answersText[i].text = currentQuestion.WrongAnswer1;
                    firstWrongAnswerAdded = true;
                }
            }
        }
    }

    public void InputAnswer(int answerIndex)
    {
        chosenAnswerIndex = answerIndex;

        // If answer was correct
        if(chosenAnswerIndex == correctAnswerIndex)
        {
            // Add 100 to the current score
            scoreManager.ModifyScore(correctAnswerScore);

            Debug.Log("Question correct!");

            // Increase the correct answers for the current topic
            scoreManager.IncrementTopicCorrectAnswers();

            // Display that the user has answered correctly to the UI
            answerAnimator.CorrectAnswer();
        }
        else
        {
             Debug.Log("Question wrong!");

            // Increase the incorrect answers for the current topic
            scoreManager.IncrementTopicIncorrectAnswers();

            // Display that the user has answered incorrectly to the UI
            answerAnimator.IncorrectAnswer();
        }

        NextQuestion(false); // Move onto the next question

        chosenAnswerIndex = -1;
        time = 0;

        HideQuestion();
    }

    void HideQuestion()
    {
        // Set all text to invisible
        playerMovement.IsQuestionPhase = false;
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
        // If a question is not visible, increase the timer
        if(!questionVisible)
            time += Time.deltaTime;

        // Show the question after a certain time interval
        if(time >= questionInterval && !questionVisible)
        {
            DisplayQuestion();
            time = 0;
        }
    }
}
