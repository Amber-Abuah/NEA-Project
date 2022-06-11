using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RevisionLink : MonoBehaviour
{
    [SerializeField] string link;
    [SerializeField] int revisionLinkID;

    public string Link { get { return link; }}
    public int RevisionLinkID { get { return revisionLinkID; }}

    public RevisionLink(string revisionLink, int linkID)
    {
        link = revisionLink;
        revisionLinkID = linkID; 
    }
}
