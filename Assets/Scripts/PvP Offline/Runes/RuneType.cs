using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RuneType
{
    [SerializeField]
    private RuneClass runeClass;
    [SerializeField]
    private RuneAppearance runeAppearance;

    public RuneClass RuneClass => runeClass;

    public RuneType(RuneClass rClass, RuneAppearance rApp)
    {
        runeClass = rClass;
        runeAppearance = rApp;
    }
}
