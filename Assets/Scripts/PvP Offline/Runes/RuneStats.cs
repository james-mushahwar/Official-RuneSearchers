using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RuneStats
{
    [SerializeField]
    private int hitPoints;
    [SerializeField]
    private int runeDrops;

    public int HitPoints => hitPoints;
    public int RuneDrops => runeDrops;

    public RuneStats(int hp, int drops)
    {
        hitPoints = hp;
        runeDrops = drops;
    }

    public void TakeDamage()
    {
        Debug.Log("Rune take damage");
        hitPoints--;
    }
}
