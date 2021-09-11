using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class Searcher : Card, IDestructable, IMoveable, ICombatant, ITrader, IExtract
{
    #region Variables
    // IDestructable
    public int hitPoints { get; set; }
    public int maxHitPoints { get; set; }
    public ITakeDamage damage { get; set; }
    // IMoveable
    public int moveRange { get; set; }
    public int remainingMoves { get; set; }
    public int allowedMoves { get; set; }
    // ICombatant
    public int attack { get; set; }
    public int remainingAttacks { get; set; }
    public int allowedAttacks { get; set; }
    public int attackRange { get; set; }
    // ITrader
    public IRuneEnergy runeEnergy { get; set; }
    public bool sent { get; set; }
    public bool received { get; set; }
    // IExtract
    public bool extracted { get; set; }
    public bool extracting { get; set; }
    #endregion

    #region Functions
    // IExtract
    public virtual bool CanExtract() { return false; }
    public virtual void StartExtract() { }
    public virtual void CancelExtract() { }
    #endregion
}

public interface IExtract
{
    bool extracted { get; set; }
    bool extracting { get; set; }
    bool CanExtract();
    void StartExtract();
    void CancelExtract();
}