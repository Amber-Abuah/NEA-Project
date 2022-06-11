using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevisionItemManager : MonoBehaviour
{
    DatabaseManipulator databaseManipulator;

    [SerializeField] QuestionManager questionManager;

    [SerializeField] List<RevisionLink> logicGateLinks;
    [SerializeField] List<RevisionLink> binaryLinks;
    [SerializeField] List<RevisionLink> booleanAlgebraLinks;
    [SerializeField] List<RevisionLink> soundLinks;
    [SerializeField] List<RevisionLink> dataRepresentationLinks;

    [SerializeField] List<RevisionLink> linksPickedUp;

    public List<RevisionLink> LinksPickedUp { get { return linksPickedUp; } }

    void Start()
    {
        databaseManipulator = new DatabaseManipulator();

        // Initialize all lists
        logicGateLinks = new List<RevisionLink>();
        binaryLinks = new List<RevisionLink>();
        booleanAlgebraLinks = new List<RevisionLink>();
        soundLinks = new List<RevisionLink>();
        dataRepresentationLinks = new List<RevisionLink>();

        // Retrieve all links from the database for each topic
        logicGateLinks = RetrieveLinksFromDatabase(1);
        binaryLinks = RetrieveLinksFromDatabase(2);
        booleanAlgebraLinks = RetrieveLinksFromDatabase(3);
        soundLinks = RetrieveLinksFromDatabase(4);
        dataRepresentationLinks = RetrieveLinksFromDatabase(5);

        //for (int i = 0; i < logicGateLinks.Count; i++)
        //{
        //    Debug.Log("Link " + logicGateLinks[i].Link);
        //    Debug.Log("Rev ID " + logicGateLinks[i].RevisionLinkID);
        //}
    }

    List<RevisionLink> RetrieveLinksFromDatabase(int index)
    {
        return databaseManipulator.RetrieveRevisionLinks(index);
    }

    public void AddRandomLink()
    {
        // Generates random link from database, based on current question topic
        switch (questionManager.CurrentTopic)
        {
            case "Logic Gates":
                linksPickedUp.Add(logicGateLinks[Random.Range(0, (logicGateLinks.Count - 1))]);
                break;
            case "Binary":
                linksPickedUp.Add(binaryLinks[Random.Range(0, (binaryLinks.Count - 1))]);
                break;
            case "Boolean Algebra":
                linksPickedUp.Add(booleanAlgebraLinks[Random.Range(0, (booleanAlgebraLinks.Count - 1))]);
                break;
            case "Sound":
                linksPickedUp.Add(soundLinks[Random.Range(0, (soundLinks.Count - 1))]);
                break;
            case "Data Representation":
                linksPickedUp.Add(dataRepresentationLinks[Random.Range(0, (dataRepresentationLinks.Count - 1))]);
                break;
        }
    }

    public void WriteLinksPickedUpToDatabase()
    {
        for (int i = 0; i < linksPickedUp.Count; i++)
        {
            if(PlayerInformation.Instance != null)
                databaseManipulator.PerformInsertRevisionLinkCommand(PlayerInformation.Instance.PlayerID, linksPickedUp[i].RevisionLinkID);
        }
    }
}
