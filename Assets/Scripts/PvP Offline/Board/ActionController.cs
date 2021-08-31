using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private SelectHandler selectHandler;
    [SerializeField]
    private TurnController turnController;
    [SerializeField]
    private MovementController movementController;
    [SerializeField]
    private AttackController attackController;
    [SerializeField]
    private SelectionHighlight selectionHighlight;
    [SerializeField]
    private BoardManager boardManager;
    [SerializeField]
    private PlayerManager playerManager;

    public PlayerManager PlayerManager { get => playerManager; }
    public BoardManager BoardManager { get => boardManager; }

    public void DetermineSelectAction()
    {
        GameObject currentBoardSquare = selectHandler.CurrentBoardSquare;
        GameObject currentUnitSelected = selectHandler.CurrentUnitSelected;
        UnitState newUnitState = null;
        if (selectHandler.NewUnitSelected != null)
        {
            newUnitState = selectHandler.NewUnitSelected.GetComponent<InplayUnit>().UnitState;
        }
        if (selectHandler.NewBoardSquare == null)
        {
            selectHandler.NewBoardSquare = boardManager.GetBoardSquares(newUnitState.BoardRow, newUnitState.BoardColumn);
        }
        GameObject newBoardSquare = selectHandler.NewBoardSquare;
        GameObject newUnitSelected = selectHandler.NewUnitSelected;

        if (currentBoardSquare == newBoardSquare && currentUnitSelected == newUnitSelected &&
            currentBoardSquare != null && currentUnitSelected != null)
        {
            //same unit must want to unselect
            selectHandler.UnselectCurrentBoardSquare();
            boardManager.RemoveHighlightingFromAll();
            turnController.CurrentPlayer.SpawnHandler.SetInSpawnMode(false);
            turnController.CurrentPlayer.SpawnHandler.UpdateSpawnTrayUnits();
            return;
        }
        else if (boardManager.HighlightedBoardSquares.Contains(newBoardSquare) &&
                boardManager.HighlightedBoardSquares.Count != 0)
        {
            if (turnController.CurrentPlayer.SpawnHandler.IsInSpawnMode())
            {
                SpawnUnit(newBoardSquare);
            }
            else
            {
                if (newUnitSelected == null && boardManager.GetRuneOnBoardSquare(newBoardSquare) == null)
                {
                    // move unit
                    MoveUnit(currentUnitSelected, newBoardSquare, currentBoardSquare);
                }
                else if (!UnitBelongsToCurrentPlayer(newUnitState) || boardManager.GetRuneOnBoardSquare(newBoardSquare) != null)
                {
                    // attack unit
                    Attack(newUnitState, currentUnitSelected, newUnitSelected, newBoardSquare);
                }
                else if (UnitBelongsToCurrentPlayer(newUnitState))
                {
                    // transit rune
                    TransitRune(currentUnitSelected, newUnitSelected);
                }
            }
            
            selectHandler.UnselectCurrentBoardSquare();
            turnController.CurrentPlayer.SpawnHandler.SetInSpawnMode(false);
            turnController.CurrentPlayer.SpawnHandler.UpdateSpawnTrayUnits();
            boardManager.RemoveHighlightingFromAll();
        }
        else
        {
            if (!UnitBelongsToCurrentPlayer(newUnitState))
            {
                boardManager.RemoveHighlightingFromAll();
                selectHandler.UnselectCurrentBoardSquare();
                return;
            }
            boardManager.RemoveHighlightingFromAll();
            selectHandler.SelectBoardSquare(newBoardSquare, newUnitSelected);
            selectionHighlight.HighlightSurroundingBoardSquares(newBoardSquare, newUnitSelected);
            turnController.CurrentPlayer.SpawnHandler.SetInSpawnMode(false);
            turnController.CurrentPlayer.SpawnHandler.UpdateSpawnTrayUnits();
        }
    }

    private void SpawnUnit(GameObject spawnBoardSquare)
    {
        Unit spawningUnit = turnController.CurrentPlayer.SpawnHandler.CurrentSelectedUnitSpawn;
        BoardSquareContainer newBoardSquareContainer = spawnBoardSquare.GetComponent<BoardSquareContainer>();

        InplayUnit spawningInplayUnit = turnController.CurrentPlayer.PlayerSpawns.GetUnitToSummon(spawningUnit.UnitStats.RuneClass, spawningUnit.UnitStats.SummonCost);
        turnController.CurrentPlayer.SpawnHandler.Spawner.SpawnUnit(spawningInplayUnit.gameObject, spawnBoardSquare, newBoardSquareContainer.RowIndex, newBoardSquareContainer.ColumnIndex);

        // exhaust runes
        turnController.CurrentPlayer.PlayerStats.ExhaustRuneClassRunes(spawningUnit.UnitStats.RuneClass, spawningUnit.UnitStats.SummonCost);
    }

    public bool UnitBelongsToCurrentPlayer(UnitState unitState)
    {
        if (unitState == null)
        {
            return false;
        }
        if (turnController.TurnState.BattleState == BattleStates.Player1Turn && unitState.UnitOccupant == Occupant.Player1)
        {
            return true;
        }
        else if (turnController.TurnState.BattleState == BattleStates.Player2Turn && unitState.UnitOccupant == Occupant.Player2)
        {
            return true;
        }

        return false;
    }

    private void MoveUnit(GameObject currentUnit, GameObject newSquare, GameObject currentSquare)
    {
        if (currentUnit.GetComponent<InplayUnit>().UnitState.CanMove())
        {
            movementController.MoveUnit(currentUnit, newSquare);
            boardManager.UnitMoved(currentUnit, newSquare, currentSquare);
        }
        selectHandler.ClearAllSelections();
    }

    private void Attack(UnitState unitState, GameObject currentUnit, GameObject attackedObject, GameObject newBoardSquare)
    {
        if (!UnitBelongsToCurrentPlayer(unitState) && unitState != null)
        {
            // attack enemy unit
            if (currentUnit.GetComponent<InplayUnit>().UnitState.CanAttack())
            {
                attackController.AttackUnit(currentUnit, attackedObject, newBoardSquare);
            }
        }
        else
        {
            // attack inplay rune
            GameObject attackedRune = newBoardSquare.GetComponent<BoardSquareContainer>().BoardRune;
            attackController.AttackRune(currentUnit, attackedRune, newBoardSquare);
        }
    }

    private void TransitRune(GameObject currentUnit, GameObject newUnit)
    {
        InplayUnit currentInplayUnit = currentUnit.GetComponent<InplayUnit>();
        InplayUnit newInplayUnit = newUnit.GetComponent<InplayUnit>();
        
        if (currentInplayUnit.HeldRunes.HasNoRunes() && !currentInplayUnit.UnitState.HasExcavated)
        {
            return;
        }

        if (currentInplayUnit.CurrentUnitStats.UnitType == UnitType.Minion)
        {
            if (newInplayUnit.CurrentUnitStats.UnitType == UnitType.Minion)
            {
                if (newInplayUnit.HeldRunes.RuneCount == 0)
                {
                    newInplayUnit.HeldRunes.RuneCount = currentInplayUnit.HeldRunes.RuneCount;
                    newInplayUnit.HeldRunes.ExhaustedCount = currentInplayUnit.HeldRunes.ExhaustedCount;
                    newInplayUnit.HeldRunes.RuneClass = currentInplayUnit.HeldRunes.RuneClass;

                    currentInplayUnit.HeldRunes.DisposeAllRunes();
                    currentInplayUnit.UnitState.HasExcavated = true;
                }
            }
            else if (newInplayUnit.CurrentUnitStats.UnitType == UnitType.Searcher)
            {
                
            }
        }
    }

    public void RemoveUnitFromBoard(GameObject removeUnit)
    {
        boardManager.RemoveUnit(removeUnit);
    }
    public void RemoveRuneFromBoard(GameObject removeRune)
    {
        boardManager.RemoveRune(removeRune);
    }

    public void SelectedUnitToSpawn()
    {
        Unit selectedSpawnUnit = turnController.CurrentPlayer.SpawnHandler.CurrentSelectedUnitSpawn;
        int unitSpawnCost = selectedSpawnUnit.UnitStats.SummonCost;
        RuneClass unitRuneClass = selectedSpawnUnit.UnitStats.RuneClass;
        Debug.Log("Selected unit to spawn");

        if (CanAffordSpawn(unitRuneClass, unitSpawnCost))
        {
            Debug.Log("Unit can be spawned - highlight area");
            GameObject searcherUnit = turnController.CurrentPlayer.PlayerSpawns.GetSearcher(unitRuneClass);
            InplayUnit inplaySearcherUnit = searcherUnit.GetComponent<InplayUnit>();
            GameObject boardSquare = boardManager.GetBoardSquares(inplaySearcherUnit.UnitState.BoardRow, inplaySearcherUnit.UnitState.BoardColumn);

            selectionHighlight.HighlightSpawnableBoardSquares(boardSquare);
        }
    }

    private bool CanAffordSpawn(RuneClass runeClass, int unitSpawnCost)
    {
        if (runeClass == RuneClass.Red)
        {
            return turnController.CurrentPlayer.PlayerStats.RedRunes.HasEqualOrMoreActiveRunes(unitSpawnCost);
        }
        else if (runeClass == RuneClass.Yellow)
        {
            return turnController.CurrentPlayer.PlayerStats.YellowRunes.HasEqualOrMoreActiveRunes(unitSpawnCost);
        }
        else
        {
            return turnController.CurrentPlayer.PlayerStats.BlueRunes.HasEqualOrMoreActiveRunes(unitSpawnCost);
        }
    }
}
