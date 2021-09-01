using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class Rune : IDestructable, IRuneEnergy
{
    // IDestructable
    public int hitPoints { get; set; }
    public int maxHitPoints { get; set; }
    // IRuneEnergy
    public int runes { get; set; }
    public int energy { get; set; }

    public GlobalEnums.RuneType runeType;
    public int deposits; // no. of runes due to be extracted
}

public interface IRuneEnergy
{
    int runes { get; set; }
    int energy { get; set; }
}
