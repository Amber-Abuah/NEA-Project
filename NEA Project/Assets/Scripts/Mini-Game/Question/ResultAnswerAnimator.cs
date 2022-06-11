using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultAnswerAnimator : MonoBehaviour
{
    [SerializeField] Animator answerAnimator;
    [SerializeField] TextMeshProUGUI answerText;

    [SerializeField] AudioSource correctAudio, incorrectAudio;

    public void CorrectAnswer()
    {
        ShowAnswerAnimation();

        // Set result answer's text and color
        answerText.text = "Correct Answer!";
        answerText.color = Color.green;

        // Play the correct answer sound effect
        correctAudio.Play();
    }

    public void IncorrectAnswer()
    {
        ShowAnswerAnimation();

        // Set result answer's text and color
        answerText.text = "Incorrect Answer...";
        answerText.color = Color.red;

        // Play the incorrect answer sound effect
        incorrectAudio.Play();
    }

    void ShowAnswerAnimation()
    {
        // Start the result answer animation
        answerAnimator.SetTrigger("showAnswer");
    }

}
