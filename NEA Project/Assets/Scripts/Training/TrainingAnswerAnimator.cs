using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainingAnswerAnimator : MonoBehaviour
{
    [SerializeField] Animator answerAnimator;
    [SerializeField] TextMeshProUGUI answerText;

    [SerializeField] AudioSource correctAudio, incorrectAudio;

    public void GoodAnswer()
    {
       // answerAnimator.SetTrigger("showAnswer");
     //   answerText.text = "Good!";
      //  answerText.color = Color.green;
        correctAudio.Play();
    }
    public void CorrectAnswer()
    {
        answerAnimator.SetTrigger("showAnswer");
        answerText.text = "Correct Answer!";
        answerText.color = Color.green;
        correctAudio.Play();
    }

    public void IncorrectAnswer()
    {
        answerAnimator.SetTrigger("showAnswer");
        answerText.text = "Incorrect Answer...";
        answerText.color = Color.red;
        incorrectAudio.Play();
    }

}
