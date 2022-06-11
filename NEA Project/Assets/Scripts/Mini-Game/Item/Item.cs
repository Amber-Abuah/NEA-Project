using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    [SerializeField] int itemPriority;

    ItemManager itemManager;

    public string ItemName { get { return itemName; } set { itemName = value; } }
    public string ItemDescription { get { return itemDescription; } set { itemDescription = value; } }
    public int ItemPriority { get { return itemPriority; } set { itemPriority = value; } }

    void Start()
    {
        if (itemManager == null)
            itemManager = ItemManager.Instance;
    }

    void OnTriggerEnter()
    {
        //Debug.Log("Player entered trigger zone!");

        // Clean up for doc!
        if(itemManager != null)
            itemManager.AddItem(this);
        else
            TrainingItemManager.Instance.AddItem(this);

        gameObject.SetActive(false);
    }
}
