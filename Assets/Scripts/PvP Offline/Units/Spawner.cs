using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawner", menuName = "Spawn/Spawner")]
public class Spawner : ScriptableObject
{
    public void SpawnUnit(GameObject unitSpawn, GameObject boardSquare, int row, int column)
    {
        boardSquare.GetComponent<BoardSquareContainer>().Unit = unitSpawn;
        unitSpawn.transform.SetParent(boardSquare.transform, false);
        unitSpawn.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        if (unitSpawn.GetComponent<InplayUnit>().UnitState.UnitOccupant == Occupant.Player2)
        {
            unitSpawn.GetComponent<SpriteRenderer>().flipX = true;
        }
        unitSpawn.GetComponent<InplayUnit>().UnitState.SetUnitCoordinates(row, column);
        unitSpawn.SetActive(true);
        unitSpawn.GetComponent<InplayUnit>().UnitState.SpawnStartState();
    }
}
