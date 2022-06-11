using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LinkDisplayManager : MonoBehaviour
{
    [SerializeField] RevisionItemManager revisionItemManager;

    [SerializeField] Transform linkDisplayParent;
    [SerializeField] GameObject linkDisplayPrefab;

    public void ShowLinks()
    {
        for (int i = 0; i < revisionItemManager.LinksPickedUp.Count; i++)
        {
            GameObject obj = Instantiate(linkDisplayPrefab) as GameObject;
            obj.transform.SetParent(linkDisplayParent);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = revisionItemManager.LinksPickedUp[i].Link;
        }
    }
}
