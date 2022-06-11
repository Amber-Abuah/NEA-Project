using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGraphVertex : MonoBehaviour
{
    [SerializeField] string date;
    [SerializeField] int scoreValue;
    [SerializeField] LineGraphVertexTooltip nodeTooltip;

    public void ChangeDateAndScore(string newDate, int newScore)
    {
        date = newDate;
        scoreValue = newScore;

        nodeTooltip.ChangeTooltipText(newDate, newScore);
    }

    public void ChangeDateAndAnswers(string newDate, string answers)
    {
        date = newDate;

        nodeTooltip.ChangeTooltipText(newDate, answers);
    }

    public void OnCursorEnter()
    {
        nodeTooltip.ShowAllElements();
    }

    public void OnCursorExit()
    {
        nodeTooltip.HideAllElements();
    }

}
