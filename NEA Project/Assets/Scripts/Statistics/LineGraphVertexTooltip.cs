using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LineGraphVertexTooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dateText;
    [SerializeField] TextMeshProUGUI valueText;

    Image backgroundColourImage;

    void Start()
    {
        backgroundColourImage = GetComponent<Image>();
        HideAllElements();
    }

    public void ChangeTooltipText(string date, int score)
    {
        // Set the date and score data on the tooltip the the current data being passed in as parameters
        dateText.text = date;
        valueText.text = score.ToString();
    }

    public void ChangeTooltipText(string date, string answers)
    {
        // Set the date and answer data on the tooltip the the current data being passed in as parameters
        dateText.text = date;
        valueText.text = answers;
    }

    public void HideAllElements()
    {
        // Make this tooltip invisible
        dateText.gameObject.SetActive(false);
        valueText.gameObject.SetActive(false);
        backgroundColourImage.color = new Color(backgroundColourImage.color.r, backgroundColourImage.color.g, backgroundColourImage.color.b, 0); // Change the colour of the background of this tooltip to invisible (an alpha value of 0)
    }

    public void ShowAllElements()
    {
        // Make the tooltip visible
        dateText.gameObject.SetActive(true);
        valueText.gameObject.SetActive(true);
        backgroundColourImage.color = new Color(backgroundColourImage.color.r, backgroundColourImage.color.g, backgroundColourImage.color.b, 1); // Make the background colour visible again
    }
}
