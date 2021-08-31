using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayUnitData : MonoBehaviour
{
    [SerializeField]
    private Unit unit;
    [SerializeField]
    private bool isPlayable;

    public Unit Unit
    {
        get => unit;
        set => unit = value;
    }

    public bool IsPlayable => isPlayable;

    public void SetIsPlayable(bool cond)
    {
        isPlayable = cond;
    }
}
