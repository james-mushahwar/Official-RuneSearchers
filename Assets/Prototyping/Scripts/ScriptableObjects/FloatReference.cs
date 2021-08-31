using System;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FloatReference
{
    public bool UseConstant = true;
    public float ConstantValue;
    public FloatVariable Variable;

    public float Value
    {
        get { return UseConstant ? ConstantValue : 
                                   Variable.CurrentValue; }
        set
        {
            if (UseConstant)
                ConstantValue = ConstantValue - value;
            else
                Variable.CurrentValue = value;
        }
    }
}
