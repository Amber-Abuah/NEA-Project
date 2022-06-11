using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public void HideGameObject()
    {
        gameObject.SetActive(false);
    }
}
