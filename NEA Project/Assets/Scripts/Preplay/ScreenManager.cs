using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] QuestionSubjectSelect questionSubjectSelect;
    [SerializeField] GameObject [] screens;

    public void ChangeScreen(int screenIndex)
    {
        // If no subjects have been selected, don't let the player move onto the next screen
        if (screenIndex == 1 && !questionSubjectSelect.CanProgress)
            return;
            
        for (int i = 0; i < screens.Length; i++)
        {
            if (i == screenIndex)
                screens[i].SetActive(true);
            else
                screens[i].SetActive(false);
        }
    }
}
