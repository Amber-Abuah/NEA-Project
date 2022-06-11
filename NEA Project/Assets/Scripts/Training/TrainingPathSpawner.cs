using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingPathSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] pathPrefabs;
    [SerializeField] GameObject[] pathGameObjects;

    [SerializeField] GameObject answerPathPrefab;
    [SerializeField] GameObject answerPathGameObject;

    [SerializeField] int startingNumOfPaths;

    ObjectPooler pathObjectPooler;
    GameObject currentPath;

    bool isQuestionPhase;

    [SerializeField] List<GameObject> currentActivePaths;

    // Singleton instance
    static TrainingPathSpawner instance;
    public static TrainingPathSpawner Instance { get { return instance; } }
    public bool IsQuestionPhase { get { return isQuestionPhase; } set { isQuestionPhase = value; } }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InstantiatePathGameObjects();
        answerPathGameObject = Instantiate(answerPathPrefab) as GameObject;
        answerPathGameObject.SetActive(false);

        pathObjectPooler = new ObjectPooler(5, pathGameObjects);

        isQuestionPhase = false;

        // Show the paths at the start of the game
        StartPaths();
    }

    void StartPaths()
    {
        // Show the first path at the position (0, 0, 0)
        ShowNextPath(Vector3.zero);

        // Show the next paths, positioned after each other in a row
        for (int i = 0; i < startingNumOfPaths; i++)
        {
            ShowNextPath(currentPath.GetComponent<TrainingPath>().NextBridgePosition.position);
        }
    }

    void InstantiatePathGameObjects()
    {
        pathGameObjects = new GameObject[pathPrefabs.Length];

        // Spawn paths and set them to deactive/ invisible
        for (int i = 0; i < pathGameObjects.Length; i++)
        {
            pathGameObjects[i] = Instantiate(pathPrefabs[i]) as GameObject;
            pathGameObjects[i].SetActive(false);
        }
    }

    void ShowNextPath(Vector3 newPosition)
    {
        // Retrieve the next path not currently being used
        RetrieveNextPath();

        // Position this path in front of the previous path
        if (currentPath != null)
        {
            ArrangePath(newPosition);
        }
    }

    public void ShowNextPath()
    {
        // Work out the position to show the next path at 
        Vector3 posToSpawn = currentPath.GetComponent<TrainingPath>().NextBridgePosition.position;

        if (isQuestionPhase && !answerPathGameObject.activeSelf)
            currentPath = answerPathGameObject;
        else
            RetrieveNextPath();

        if (currentPath != null)
        {
            ArrangePath(posToSpawn);
        }
    }

    void RetrieveNextPath()
    {
        currentPath = pathObjectPooler.RetrievePooledObject();
    }

    void ArrangePath(Vector3 pos)
    {
        currentActivePaths.Add(currentPath); // Add this path to the list of visible paths
        currentPath.SetActive(true); // Set path to be visible
        currentPath.GetComponent<TrainingPath>().ShowItem(); // Show a pick up item on this path
        currentPath.transform.position = pos; // Set the position of this path
    }
}
