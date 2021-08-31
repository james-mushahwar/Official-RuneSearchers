using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatVariable : ScriptableObject
{
    [SerializeField]
    private float originalValue;
    [SerializeField]
    private float currentValue;
    public bool Reset = true;

    public float OriginalValue
    {
        get { return originalValue; }
    }

    public float CurrentValue
    {
        get { return currentValue; }
        set { currentValue -= value; }
    }

    private void OnEnable()
    {
        ResetSO();
    }

    private void ResetSO()
    {
        if (Reset)
            currentValue = OriginalValue;
    }
}
