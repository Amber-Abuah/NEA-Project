using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    PriorityQueue<Item> items;

    private void Start()
    {
        items = new PriorityQueue<Item>(5);

        items.Enqueue(new SpeedModifierItem { ItemPriority = 1 });
        items.Enqueue(new SpeedModifierItem { ItemPriority = 3 });
        items.Enqueue(new SpeedModifierItem { ItemPriority = 7 });
        items.Enqueue(new SpeedModifierItem { ItemPriority = -1 });
        items.Enqueue(new SpeedModifierItem { ItemPriority = -2 });

        items.ViewQueue();

        Debug.Log(items.Dequeue().ItemPriority);

        items.ViewQueue();
    }
}
