using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModifierItem : Item
{
    [SerializeField] float maxSpeedAmount;
    public float MaxSpeedAmount { get { return maxSpeedAmount; } set { maxSpeedAmount = value; } }
}
