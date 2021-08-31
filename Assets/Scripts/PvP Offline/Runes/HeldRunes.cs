using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HeldRunes
{
    [SerializeField]
    private int runeCount;
    [SerializeField]
    private int exhaustedCount;
    [SerializeField]
    private RuneClass runeClass;

    public HeldRunes()
    {
        runeCount = 0;
        exhaustedCount = 0;
        runeClass = RuneClass.None;
    }

    public int RuneCount { get => runeCount; set => runeCount = value; }
    public RuneClass RuneClass { get => runeClass; set => runeClass = value; }
    public int ExhaustedCount { get => exhaustedCount; set => exhaustedCount = value; }

    public bool HasNoRunes()
    {
        return runeCount == 0;
    }

    public void DisposeAllRunes()
    {
        runeCount = 0;
        exhaustedCount = 0;
        runeClass = RuneClass.None;
    }
}
