using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Card, IDestructable, IMoveable, ICombatant, IPlayable
{
    // IDestructable
    public int hitPoints { get; set; }
    public int maxHitPoints { get; set; }
    // IMoveable
    public int moveRange { get; set; }
    public int remainingMoves { get; set; }
    public int allowedMoves { get; set; }
    // ICombatant
    public int attack { get; set; }
    public int remainingAttacks { get; set; }
    public int allowedAttacks { get; set; }
    public int attackRange { get; set; }
    // IPlayable
    public int cost { get; set; }
    public int allowedPlays { get; set; }
}
