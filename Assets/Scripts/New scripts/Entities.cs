using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public string id;
    public string name;
    public GlobalEnums.RuneType runeType;
    public GlobalEnums.RaceType raceType;
    public string description;
}

public interface IDestructable
{
    int hitPoints { get; set; }
    int maxHitPoints { get; set; }
}

public interface IMoveable
{
    int moveRange { get; set; }
    int remainingMoves { get; set; }
    int allowedMoves { get; set; }
}

public interface ICombatant
{
    int attack { get; set; }
    int remainingAttacks { get; set; }
    int allowedAttacks { get; set; }
    int attackRange { get; set; }
}

public interface IPlayable
{
    int cost { get; set; }
    int allowedPlays { get; set; }
}

public interface IRuneEnergy
{
    int runes { get; set; }
    int energy { get; set; }
}

