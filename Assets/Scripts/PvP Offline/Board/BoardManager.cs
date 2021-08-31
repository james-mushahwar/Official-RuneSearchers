using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int BoardRows { get; set; }
    public int BoardColumns { get; set; }

    private GameObject[][] boardSquares;
    [SerializeField]
    private List<GameObject> highlightedBoardSquares = new List<GameObject>();
    [SerializeField]
    private List<GameObject> summonBoardSquares = new List<GameObject>();

    public GameObject[][] BoardSquares
    {
        get { return boardSquares; }
        set { boardSquares = value; }
    }

    public List<GameObject> HighlightedBoardSquares
    {
        get { return highlightedBoardSquares; }
    }

    public GameObject GetBoardSquares(int row, int column)
    {
        return boardSquares[row][column];
    }

    public void SetBoardSquares(int row, int column, GameObject newSquare)
    {
        boardSquares[row][column] = newSquare;
    }

    public GameObject GetUnitOnBoardSquare(int row, int column)
    {
        return boardSquares[row][column].GetComponent<BoardSquareContainer>().Unit;
    }

    public GameObject GetUnitFromBoardSquare(GameObject boardSquare)
    {
        return boardSquare.GetComponent<BoardSquareContainer>().Unit;
    }

    public void RemoveUnit(GameObject boardSquare)
    {
        boardSquare.GetComponent<BoardSquareContainer>().Unit = null;
    }

    public BoardSquareContainer GetContainerOnBoardSquare(int row, int column)
    {
        return boardSquares[row][column].GetComponent<BoardSquareContainer>();
    }

    public GameObject GetRuneOnBoardSquare(GameObject boardSquare)
    {
        return boardSquare.GetComponent<BoardSquareContainer>().BoardRune;
    }

    public void RemoveHighlightingFromAll()
    {
        foreach (GameObject boardSquare in highlightedBoardSquares)
        {
            boardSquare.GetComponent<Highlight>().RemoveHighlighting();
        }
        highlightedBoardSquares.Clear();
    }

    public void UnitMoved(GameObject movedUnit, GameObject newSquare, GameObject currentSquare)
    {
        BoardSquareContainer newSquareContainer = newSquare.GetComponent<BoardSquareContainer>();
        BoardSquareContainer currentSquareContainer = currentSquare.GetComponent<BoardSquareContainer>();

        newSquareContainer.Unit = movedUnit;
        currentSquareContainer.Unit = null;
        movedUnit.GetComponent<InplayUnit>().UnitState.SetUnitCoordinates(newSquareContainer.RowIndex, newSquareContainer.ColumnIndex);
    }

    public void AddGridSquareToHiglighted(GameObject gridToAdd)
    {
        if (highlightedBoardSquares.Contains(gridToAdd) == false)
        {
            highlightedBoardSquares.Add(gridToAdd);
        }
    }

    public bool IsEmptyOccupant(int row, int column)
    {
        return (boardSquares[row][column].GetComponent<BoardSquareContainer>().Unit == null && IsEmptyRuneOccupant(row, column));
    }

    public bool IsEmptyRuneOccupant(int row, int column)
    {
        return boardSquares[row][column].GetComponent<BoardSquareContainer>().BoardRune == null;
    }

    public void RemoveRune(GameObject boardSquare)
    {
        boardSquare.GetComponent<BoardSquareContainer>().BoardRune = null;
    }
}
