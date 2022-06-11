using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LinkButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI linkText;
    string linkAddress;

    public void OpenLink()
    {
        Application.OpenURL(linkAddress);
    }

    public void SetText(string linkString)
    {
        linkAddress = linkString;
        linkText.text = linkAddress;
    }
}
