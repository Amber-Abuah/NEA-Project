using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerTrigger : MonoBehaviour
{
    [SerializeField] int answerIndex;
    QuestionManager questionManager;

    void Start()
    {
        if (questionManager == null)
            questionManager = QuestionManager.Instance;
    }

    void OnTriggerEnter()
    {
        // Answer question with the path's index
       // questionManager.InputAnswer(answerIndex);

       // QuestionManager questionManager = QuestionManager.Instance;

        if (questionManager != null)
            QuestionManager.Instance.InputAnswer(answerIndex);
        else
            TrainingQuestionManager.Instance.InputAnswer(answerIndex);
    }
}
