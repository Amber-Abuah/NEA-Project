using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    [SerializeField] string question;
    [SerializeField] string correctAnswer;
    [SerializeField] string wrongAnswer1;
    [SerializeField] string wrongAnswer2;
    [SerializeField] float time;

    public string QuestionToAsk { get { return question; }}
    public string CorrectAnswer { get { return correctAnswer; }}
    public string WrongAnswer1 { get { return wrongAnswer1; }}
    public string WrongAnswer2 { get { return wrongAnswer2; }}
    public float Time { get { return time; }}
}
