using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkDisplay : MonoBehaviour
{
    DatabaseManipulator databaseManipulator;
    PlayerInformation playerInformation;
    List<string> logicGateLinks,  binaryLinks, booleanLinks, soundLinks, dataRepresentationLinks;
    [SerializeField] Transform logicGateColumn,  binaryColumn, booleanColumn, soundColumn, dataRepresentationColumn;

    [SerializeField] GameObject linkPrefab;


    void Start()
    {
        playerInformation = PlayerInformation.Instance;

        databaseManipulator = new DatabaseManipulator();

        int playerID =  playerInformation == null? 1 : playerInformation.PlayerID;

        // Retrieve all revision links from the database, under the playerID
        logicGateLinks = databaseManipulator.PerformRevisionLinksSelectCommand(playerID, 1);
        binaryLinks = databaseManipulator.PerformRevisionLinksSelectCommand(playerID, 2);
        booleanLinks = databaseManipulator.PerformRevisionLinksSelectCommand(playerID, 3);
        soundLinks = databaseManipulator.PerformRevisionLinksSelectCommand(playerID, 4);
        dataRepresentationLinks = databaseManipulator.PerformRevisionLinksSelectCommand(playerID, 5);

        // Create the hyperlink buttons under the correct columns
        CreateButtons(logicGateLinks, logicGateColumn);
        CreateButtons(binaryLinks, binaryColumn);
        CreateButtons(booleanLinks, booleanColumn);
        CreateButtons(soundLinks, soundColumn);
        CreateButtons(dataRepresentationLinks, dataRepresentationColumn);
    
    }

    void CreateButtons(List<string> links, Transform parent)
    {
        for (int i = 0; i < links.Count; i++)
        {
            // Create the button hyperlink
            GameObject obj = Instantiate(linkPrefab);

            // Position button under correct topic column
            obj.transform.SetParent(parent);

            // Set the text of the button to the link address
            obj.GetComponent<LinkButton>().SetText(links[i]);

            // Set the scale of the button so it fits within the column
            obj.transform.localScale = new Vector3(1, 1, 1);
        }

    }
}
