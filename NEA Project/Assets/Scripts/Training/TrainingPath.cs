using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingPath : MonoBehaviour
{
    [SerializeField] Transform nextBridgePosition;
    [SerializeField] Transform[] items;

    TrainingPathSpawner pathSpawner;

    public Transform NextBridgePosition { get { return nextBridgePosition; } }

    void Start() // Called everytime the gameObject is activated
    {
        if (pathSpawner == null)
            pathSpawner = TrainingPathSpawner.Instance;
    }

    // Called when the player reaches the end of this path
    void OnTriggerEnter(Collider other)
    {
        pathSpawner.ShowNextPath(); // Tell the path spawner to show the next path
        gameObject.SetActive(false); // Deactivate this game object
    }

    public void ShowItem()
    {
        HideAllItems();

        if (items.Length != 0)
        {

            bool showItem = Random.Range(0, 11) < 3 ? true : false; // 3 in 10 chance that an item will appear

            if (showItem)
            {
                int randomNum = Random.Range(0, items.Length); // Generate random item index
                items[randomNum].gameObject.SetActive(true); // Activate the item at index randomNum in items array
            }
        }
    }

    // Deactivate all item game objects
    void HideAllItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].gameObject.SetActive(false);
        }
    }
}
