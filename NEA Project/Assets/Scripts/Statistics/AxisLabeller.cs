using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AxisLabeller : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI [] percentageText;

    public void ChangeText(int maxScore)
    {
        percentageText[0].text = "0";
        percentageText[1].text = (maxScore / 4).ToString();
        percentageText[2].text = (maxScore / 2).ToString();
        percentageText[3].text = ((maxScore / 4) * 3).ToString();
        percentageText[4].text = maxScore.ToString();
    }
}
