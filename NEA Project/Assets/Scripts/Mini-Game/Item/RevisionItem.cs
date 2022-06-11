using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevisionItem : Item
{
    [SerializeField] string revisionLink;
    public string RevisionLink { get { return revisionLink; } set { revisionLink = value; } }
}
