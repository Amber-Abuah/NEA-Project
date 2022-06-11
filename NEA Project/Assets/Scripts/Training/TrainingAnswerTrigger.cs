using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingAnswerTrigger : MonoBehaviour
{
    [SerializeField] int answerIndex;

    private void OnTriggerEnter()
    {
        TrainingQuestionManager.Instance.InputAnswer(answerIndex);
    }
}
