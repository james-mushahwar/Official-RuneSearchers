using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SearcherRune
{
    [SerializeField]
    private bool isExhausted;
    [SerializeField]
    private RuneClass runeClass;

    public bool IsExhausted { get => isExhausted; set => isExhausted = value; }
    public RuneClass RuneClass { get => runeClass; set => runeClass = value; }

    public SearcherRune(bool cond, RuneClass rClass)
    {
        isExhausted = cond;
        runeClass = rClass;
    }
    public SearcherRune()
    {
        isExhausted = false;
        runeClass = RuneClass.None;
    }

    public void Exhaust()
    {
        isExhausted = true;
    }
}
