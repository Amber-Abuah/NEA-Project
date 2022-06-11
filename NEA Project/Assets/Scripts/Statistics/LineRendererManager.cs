using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererManager : MonoBehaviour, IObjectPooler<LineRenderer>
{
    [SerializeField] GameObject lineRendererPrefab;
    [SerializeField] LineRenderer[] lineRenderers;
    [SerializeField] Vector3 startVectorPos;

    const int amountOfLines = 89;

    void Start()
    {
        lineRenderers = new LineRenderer[amountOfLines];

        // Create all 89 line renderers
        for (int i = 0; i < amountOfLines; i++)
        {
            // Instantiate object
            GameObject obj = Instantiate(lineRendererPrefab) as GameObject;
            obj.transform.SetParent(gameObject.transform);

            // Set the lines starting position
            obj.transform.position = startVectorPos;

            // Retrieve the line renderer component from the game object
            lineRenderers[i] = obj.GetComponent<LineRenderer>();

            // Set line to invisible
            obj.SetActive(false); 
        }
    }

    // Finds the next available, invisible line 
    public LineRenderer RetrievePooledObject()
    {
        // Loops through all lines
        for (int i = 0; i < lineRenderers.Length; i++)
        {
            // If an invisible line is found
            if (!lineRenderers[i].gameObject.activeSelf)
            {
                // Set the line to visible
                lineRenderers[i].gameObject.SetActive(true);

                // Return current line
                return lineRenderers[i];
            }
        }
        return null;
    }

    public void HideAllLines()
    {
        // Loop through all lines and set them to invisible
        for (int i = 0; i < lineRenderers.Length; i++)
        {
            lineRenderers[i].gameObject.SetActive(false);
        }
    }
}
