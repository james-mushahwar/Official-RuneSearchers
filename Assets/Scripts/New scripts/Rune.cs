using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class Rune : IDestructable, IRuneEnergy
{
    #region Variables
    // IDestructable
    public int hitPoints { get; set; }
    public int maxHitPoints { get; set; }
    public ITakeDamage damage { get; set; }
    // IRuneEnergy
    public int runes { get; set; }
    public int energy { get; set; }
    

    // Rune variables
    public GlobalEnums.RuneType runeType;
    public GlobalEnums.RuneSize runeSize;
    public int deposits; // no. of runes due to be extracted
    #endregion

    #region Functions
    
    // Rune functions
    public Rune(GlobalEnums.RuneType runeType, GlobalEnums.RuneSize runeSize)
    {
        if (runeSize == GlobalEnums.RuneSize.Small)
        {
            hitPoints = maxHitPoints = 2;
            runes = 1;
            energy = 20;
        }
        else if (runeSize == GlobalEnums.RuneSize.Medium)
        {
            hitPoints = maxHitPoints = 3;
            runes = 2;
            energy = 50;
        }
        else if (runeSize == GlobalEnums.RuneSize.Large)
        {
            hitPoints = maxHitPoints = 4;
            runes = 3;
            energy = 100;
        }
        this.runeType = runeType;
        this.runeSize = runeSize;
        damage = new IRuneTakeDamage();
    }
    #endregion
}

public class IRuneTakeDamage : ITakeDamage
{
    public void TakeDamage(IDestructable d, int damage)
    {
        // ignore damage, just -1
        d.hitPoints--;
    }
}

public interface IRuneEnergy
{
    int runes { get; set; }
    int energy { get; set; }
}

public interface IExtractable
{
    int deposits { get; set; }
}
