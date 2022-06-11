using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainingItemManager : MonoBehaviour
{
    // Singleton instance
    static TrainingItemManager instance;
    public static TrainingItemManager Instance { get { return instance; } }

    [SerializeField] RevisionItemManager revisionItemManager;

    [SerializeField] PriorityQueue<Item> items;
    [SerializeField] TrainingPlayerMovement playerMovement;
    [SerializeField] TrainingScoreManager scoreManager;
    [SerializeField] float effectInterval;

    [SerializeField] TextMeshProUGUI[] itemText;

    Item removedItem;
    float time;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        items = new PriorityQueue<Item>(3); // Initialize priority queue with size of 3
    }

    void ApplySpeedEffect(SpeedModifierItem item)
    {
        playerMovement.ChangeMaxSpeed(item.MaxSpeedAmount);
    }

    void ApplyTimerEffect(TimeModifierItem item)
    {
    }

    void ApplyScoreEffect(ScoreModifierItem item)
    {
        scoreManager.ModifyScore(item.ScoreModifierAmount);
    }

    public void AddItem(Item item)
    {
        if (item.GetType() != typeof(RevisionItem))
        {
            // If the queue is full, remove the item at the front
            if (items.IsFull)
            {
                removedItem = items.Dequeue();

                // If the item removed had an affect on the player's speed, reset the player's maximum speed
                if (removedItem.GetType() == typeof(SpeedModifierItem))
                    playerMovement.ResetMaxSpeed();
            }

            // Add the new item
            items.Enqueue(item);

            ApplySpeedEffects();

            DisplayItemsToUI();
        }
    }

    void DisplayItemsToUI()
    {
        // Display the name and descriptions of the current items to the UI
        for (int i = 0; i < items.Size; i++)
        {
            Item currentItem = items.PeekItemAtIndex(i);
            itemText[i].text = "Item " + (i + 1).ToString() + " - " + currentItem.ItemName + " : " + currentItem.ItemDescription;
        }
    }

    void ApplySpeedEffects()
    {
        // Debug.Log("Applying speed effects...");

        // Loop through the items in the queue, if they affect speed, they will be applied in the correct order
        for (int i = 0; i < items.Size; i++)
        {
            Item currentItem = items.PeekItemAtIndex(i);

            if (currentItem.GetType() == typeof(SpeedModifierItem))
            {
                ApplySpeedEffect((SpeedModifierItem)currentItem);
                Debug.Log("Found speed effect!");
            }
        }
    }

    void ApplyTimeAndScoreItemEffects()
    {
        // Loops through all elements in queue, applying effects
        for (int i = 0; i < items.Size; i++)
        {
            Item currentItem = items.PeekItemAtIndex(i);

            if (currentItem.GetType() == typeof(ScoreModifierItem))
            {
                //Debug.Log("Score modifier found! Apply score effects!");
                ApplyScoreEffect((ScoreModifierItem)currentItem);
            }

        }
    }

    void Update()
    {
        // Increase the timer
        time += Time.deltaTime;

        // Applies time and score effects every set amount of seconds
        if (time >= effectInterval)
        {
            ApplyTimeAndScoreItemEffects();
            //  Debug.Log("Applied all time and score effects!");
            time = 0; // Reset timer
        }
    }
}
