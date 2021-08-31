using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCard : ScriptableObject
{
    [SerializeField]
    private int playCost;
    [SerializeField]
    private string cardName;
    [SerializeField]
    private InplayUnit unitOwner;

    [SerializeField]
    private CardAbility cardAbility;
}
