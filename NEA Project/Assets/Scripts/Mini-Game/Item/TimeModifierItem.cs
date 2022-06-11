using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeModifierItem : Item
{
    [SerializeField] float timeModifierAmount;
    public float TimeModifierAmount { get { return timeModifierAmount; } set { timeModifierAmount = value; } }
}
