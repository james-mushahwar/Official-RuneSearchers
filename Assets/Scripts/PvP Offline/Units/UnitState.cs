using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UnitState
{
    [Header("Board state")]
    [SerializeField]
    private int boardRow;
    [SerializeField]
    private int boardColumn;

    [Header("Player state")]
    [SerializeField]
    private Occupant unitOccupant;

    [Header("General unit state")]
    [SerializeField]
    private bool isSpawned;
    [SerializeField]
    private bool isAlive;
    [SerializeField]
    private bool hasMoved;
    [SerializeField]
    private bool hasAttacked;

    [Header("Rune action states")]
    [SerializeField]
    private bool hasTransitted;
    [SerializeField]
    private bool hasExcavated;

    public int BoardRow
    {
        get => boardRow;
        set => boardRow = value;
    }
    public int BoardColumn
    {
        get => boardColumn;
        set => boardColumn = value;
    }
    public Occupant UnitOccupant
    {
        get => unitOccupant;
        set => unitOccupant = value;
    }

    public bool IsAlive { get => isAlive; set => isAlive = value; }
    public bool IsSpawned { get => isSpawned; set => isSpawned = value; }
    public bool HasTransitted { get => hasTransitted; set => hasTransitted = value; }
    public bool HasExcavated { get => hasExcavated; set => hasExcavated = value; }

    public UnitState()
    {
        isSpawned = false;
        isAlive = false;
        hasMoved = false;
        hasAttacked = false;
        hasTransitted = false;
        hasExcavated = false;
    }

    public void SetUnitCoordinates(int newRow, int newColumn)
    {
        boardRow = newRow;
        boardColumn = newColumn;
    }

    public void SpawnStartState()
    {
        Debug.Log("I am set to ISSPAWNED");
        isSpawned = true;
        isAlive = true;
    }

    public void SetHasMoved(bool cond)
    {
        hasMoved = cond;
    }
    public bool CanMove()
    {
        return !hasMoved;
    }
    public bool GetHasMoved()
    {
        return hasMoved;
    }

    public void SetHasAttacked(bool cond)
    {
        hasAttacked = cond;
    }
    public bool CanAttack()
    {
        return !hasAttacked;
    }

    public void SetIsAlive(bool cond)
    {
        isAlive = cond;
    }
}
