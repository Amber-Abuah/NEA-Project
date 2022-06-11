using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingScreens : MonoBehaviour
{
    [SerializeField] GameObject[] screens;
    int currentScreenIndex;

    [SerializeField] TrainingAnswerAnimator anim;
    [SerializeField] TrainingPlayerMovement playerMovement;

    [SerializeField] GameObject button;

    float timer;

    private void Start()
    {
        screens[currentScreenIndex].SetActive(true);
    }

    public void NextScreen()
    {
        screens[currentScreenIndex].SetActive(false);
        currentScreenIndex++;
        screens[currentScreenIndex].SetActive(true);

        if(currentScreenIndex == 4)
        {
            TrainingQuestionManager.Instance.DisplayQuestion("What base is denary?", "10", "2", "1", 0);
        }
        else if(currentScreenIndex == 5)
        {
            button.SetActive(false);
        }
        }

}
