using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreModifierItem : Item
{
    [SerializeField] float scoreModifierAmount;
    public float ScoreModifierAmount { get { return scoreModifierAmount; } set { scoreModifierAmount = value; } }
}
