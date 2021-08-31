using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Unit", menuName = "Unit/Unit")]
public class Unit : ScriptableObject
{
    [Header("Original settings")]
    [SerializeField]
    private UnitStats unitStats;
    [SerializeField]
    private UnitAppearance unitAppearance;

    public UnitStats UnitStats => unitStats;
    public UnitAppearance UnitAppearance => unitAppearance;
}
