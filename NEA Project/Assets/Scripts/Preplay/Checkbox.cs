using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkbox : MonoBehaviour
{
    bool isSelected;
    [SerializeField] Image tickImage;

    void Start()
    {
        isSelected = false;
        tickImage.gameObject.SetActive(false);
    }

    public void Select()
    {
        isSelected = !isSelected;
        tickImage.gameObject.SetActive(isSelected);
    }
}
