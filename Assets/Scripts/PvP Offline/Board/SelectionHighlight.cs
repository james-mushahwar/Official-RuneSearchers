using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHighlight : MonoBehaviour
{
    [SerializeField]
    private ActionController actionController;

    public void HighlightSurroundingBoardSquares(GameObject newBoardSquare, GameObject newUnitSelected)
    {
        int boardRow = newBoardSquare.GetComponent<BoardSquareContainer>().RowIndex;
        int boardColumn = newBoardSquare.GetComponent<BoardSquareContainer>().ColumnIndex;
        int unitRange = newUnitSelected.GetComponent<InplayUnit>().CurrentUnitStats.MovementRange;
        bool unitCanMove = newUnitSelected.GetComponent<InplayUnit>().UnitState.CanMove();

        BoardSquareContainer boardSquare;
        List<Vector2> squaresToTraverse = new List<Vector2>();
        squaresToTraverse.Add(new Vector2(boardRow, boardColumn));

        for (int i = 0; i < unitRange; i++)
        {
            List<Vector2> nextSquaresToTraverse = new List<Vector2>();
            foreach (Vector2 selectedSquare in squaresToTraverse)
            {
                //north
                if (selectedSquare.x < actionController.BoardManager.BoardRows - 1)
                {
                    boardSquare = actionController.BoardManager.GetContainerOnBoardSquare((int)selectedSquare.x + 1, (int)selectedSquare.y);
                    DetermineBoardSquareHightlight(boardSquare, boardRow, boardColumn, nextSquaresToTraverse, unitCanMove);
                }
                //south
                if (selectedSquare.x > 0)
                {
                    boardSquare = actionController.BoardManager.GetContainerOnBoardSquare((int)selectedSquare.x - 1, (int)selectedSquare.y);
                    DetermineBoardSquareHightlight(boardSquare, boardRow, boardColumn, nextSquaresToTraverse, unitCanMove);
                }
                //east
                if (selectedSquare.y < actionController.BoardManager.BoardColumns - 1)
                {
                    boardSquare = actionController.BoardManager.GetContainerOnBoardSquare((int)selectedSquare.x, (int)selectedSquare.y + 1);
                    DetermineBoardSquareHightlight(boardSquare, boardRow, boardColumn, nextSquaresToTraverse, unitCanMove);
                }
                //west
                if (selectedSquare.y > 0)
                {
                    boardSquare = actionController.BoardManager.GetContainerOnBoardSquare((int)selectedSquare.x, (int)selectedSquare.y - 1);
                    DetermineBoardSquareHightlight(boardSquare, boardRow, boardColumn, nextSquaresToTraverse, unitCanMove);
                }
            }

            squaresToTraverse = new List<Vector2>(nextSquaresToTraverse);
        }
    }

    private void DetermineBoardSquareHightlight(BoardSquareContainer boardSquare, int boardRow, int boardColumn, List<Vector2> nextSquaresToTraverse, bool unitCanMove)
    {
        if (actionController.BoardManager.IsEmptyOccupant(boardSquare.RowIndex, boardSquare.ColumnIndex))
        {
            if (unitCanMove)
            {
                boardSquare.gameObject.GetComponent<Highlight>().ApplyHighlighting(Color.blue);
                actionController.BoardManager.AddGridSquareToHiglighted(boardSquare.gameObject);
            }
            nextSquaresToTraverse.Add(new Vector2(boardSquare.RowIndex, boardSquare.ColumnIndex));
        }
        else if (!actionController.BoardManager.IsEmptyRuneOccupant(boardSquare.RowIndex, boardSquare.ColumnIndex))
        {
            boardSquare.gameObject.GetComponent<Highlight>().ApplyHighlighting(Color.red);
            actionController.BoardManager.AddGridSquareToHiglighted(boardSquare.gameObject);
        }
        else if (!actionController.UnitBelongsToCurrentPlayer(boardSquare.Unit.GetComponent<InplayUnit>().UnitState))
        {
            boardSquare.gameObject.GetComponent<Highlight>().ApplyHighlighting(Color.red);
            actionController.BoardManager.AddGridSquareToHiglighted(boardSquare.gameObject);
        }
        else if (actionController.UnitBelongsToCurrentPlayer(boardSquare.Unit.GetComponent<InplayUnit>().UnitState) 
             && (boardSquare.RowIndex != boardRow || boardSquare.ColumnIndex != boardColumn))
        {
            if (!boardSquare.Unit.GetComponent<InplayUnit>().UnitState.HasTransitted
                && boardSquare.Unit.GetComponent<InplayUnit>().HeldRunes.HasNoRunes()
                && !actionController.BoardManager.GetBoardSquares(boardRow, boardColumn).GetComponent<BoardSquareContainer>().Unit.GetComponent<InplayUnit>().HeldRunes.HasNoRunes())
            {
                boardSquare.gameObject.GetComponent<Highlight>().ApplyHighlighting(Color.green);
                actionController.BoardManager.AddGridSquareToHiglighted(boardSquare.gameObject);
            }
            nextSquaresToTraverse.Add(new Vector2(boardSquare.RowIndex, boardSquare.ColumnIndex));
        }
    }

    public void HighlightSpawnableBoardSquares(GameObject searcherBoardSquare)
    {
        BoardSquareContainer searcherBoardSquareContainer = searcherBoardSquare.GetComponent<BoardSquareContainer>();
        int boardRow = searcherBoardSquareContainer.RowIndex;
        int boardColumn = searcherBoardSquareContainer.ColumnIndex;

        // north
        if (boardRow < actionController.BoardManager.BoardRows - 1)
        {
            if (actionController.BoardManager.GetContainerOnBoardSquare(boardRow + 1, boardColumn).Unit == null &&
                actionController.BoardManager.GetContainerOnBoardSquare(boardRow + 1, boardColumn).EnvironmentUnit == null &&
                actionController.BoardManager.GetContainerOnBoardSquare(boardRow + 1, boardColumn).BoardRune == null)
            {
                actionController.BoardManager.GetBoardSquares(boardRow + 1, boardColumn).GetComponent<Highlight>().ApplyHighlighting(Color.yellow);
                actionController.BoardManager.AddGridSquareToHiglighted(actionController.BoardManager.GetBoardSquares(boardRow + 1, boardColumn));
            }
        }
        // north-west
        if (boardRow < actionController.BoardManager.BoardRows - 1 && boardColumn > 0)
        {
            if (actionController.BoardManager.GetContainerOnBoardSquare(boardRow + 1, boardColumn - 1).Unit == null &&
                actionController.BoardManager.GetContainerOnBoardSquare(boardRow + 1, boardColumn - 1).EnvironmentUnit == null &&
                actionController.BoardManager.GetContainerOnBoardSquare(boardRow + 1, boardColumn - 1).BoardRune == null)
            {
                actionController.BoardManager.GetBoardSquares(boardRow + 1, boardColumn - 1).GetComponent<Highlight>().ApplyHighlighting(Color.yellow);
                actionController.BoardManager.AddGridSquareToHiglighted(actionController.BoardManager.GetBoardSquares(boardRow + 1, boardColumn - 1));
            }
        }
        // north-east
        if (boardRow < actionController.BoardManager.BoardRows - 1 && boardColumn < actionController.BoardManager.BoardColumns - 1)
        {
            if (actionController.BoardManager.GetContainerOnBoardSquare(boardRow + 1, boardColumn + 1).Unit == null &&
                actionController.BoardManager.GetContainerOnBoardSquare(boardRow + 1, boardColumn + 1).EnvironmentUnit == null &&
                actionController.BoardManager.GetContainerOnBoardSquare(boardRow + 1, boardColumn + 1).BoardRune == null)
            {
                actionController.BoardManager.GetBoardSquares(boardRow + 1, boardColumn + 1).GetComponent<Highlight>().ApplyHighlighting(Color.yellow);
                actionController.BoardManager.AddGridSquareToHiglighted(actionController.BoardManager.GetBoardSquares(boardRow + 1, boardColumn + 1));
            }
        }
        // south
        if (boardRow > 0)
        {
            if (actionController.BoardManager.GetContainerOnBoardSquare(boardRow - 1, boardColumn).Unit == null &&
                actionController.BoardManager.GetContainerOnBoardSquare(boardRow - 1, boardColumn).EnvironmentUnit == null &&
                actionController.BoardManager.GetContainerOnBoardSquare(boardRow - 1, boardColumn).BoardRune == null)
            {
                actionController.BoardManager.GetBoardSquares(boardRow - 1, boardColumn).GetComponent<Highlight>().ApplyHighlighting(Color.yellow);
                actionController.BoardManager.AddGridSquareToHiglighted(actionController.BoardManager.GetBoardSquares(boardRow - 1, boardColumn));
            }
        }
        // south-east
        if (boardRow > 0 && boardColumn < actionController.BoardManager.BoardColumns - 1)
        {
            if (actionController.BoardManager.GetContainerOnBoardSquare(boardRow - 1, boardColumn + 1).Unit == null &&
                actionController.BoardManager.GetContainerOnBoardSquare(boardRow - 1, boardColumn + 1).EnvironmentUnit == null &&
                actionController.BoardManager.GetContainerOnBoardSquare(boardRow - 1, boardColumn + 1).BoardRune == null)
            {
                actionController.BoardManager.GetBoardSquares(boardRow - 1, boardColumn + 1).GetComponent<Highlight>().ApplyHighlighting(Color.yellow);
                actionController.BoardManager.AddGridSquareToHiglighted(actionController.BoardManager.GetBoardSquares(boardRow - 1, boardColumn + 1));
            }
        }
        // south-west
        if (boardRow > 0 && boardColumn > 0)
        {
            if (actionController.BoardManager.GetContainerOnBoardSquare(boardRow - 1, boardColumn - 1).Unit == null &&
                actionController.BoardManager.GetContainerOnBoardSquare(boardRow - 1, boardColumn - 1).EnvironmentUnit == null &&
                actionController.BoardManager.GetContainerOnBoardSquare(boardRow - 1, boardColumn - 1).BoardRune == null)
            {
                actionController.BoardManager.GetBoardSquares(boardRow - 1, boardColumn - 1).GetComponent<Highlight>().ApplyHighlighting(Color.yellow);
                actionController.BoardManager.AddGridSquareToHiglighted(actionController.BoardManager.GetBoardSquares(boardRow - 1, boardColumn - 1));
            }
        }
        // east
        if (boardColumn < actionController.BoardManager.BoardColumns - 1)
        {
            if (actionController.BoardManager.GetContainerOnBoardSquare(boardRow, boardColumn + 1).Unit == null &&
                actionController.BoardManager.GetContainerOnBoardSquare(boardRow, boardColumn + 1).EnvironmentUnit == null &&
                actionController.BoardManager.GetContainerOnBoardSquare(boardRow, boardColumn + 1).BoardRune == null)
            {
                actionController.BoardManager.GetBoardSquares(boardRow, boardColumn + 1).GetComponent<Highlight>().ApplyHighlighting(Color.yellow);
                actionController.BoardManager.AddGridSquareToHiglighted(actionController.BoardManager.GetBoardSquares(boardRow, boardColumn + 1));
            }
        }
        // west
        if (boardColumn > 0)
        {
            if (actionController.BoardManager.GetContainerOnBoardSquare(boardRow, boardColumn - 1).Unit == null &&
                actionController.BoardManager.GetContainerOnBoardSquare(boardRow, boardColumn - 1).EnvironmentUnit == null &&
                actionController.BoardManager.GetContainerOnBoardSquare(boardRow, boardColumn - 1).BoardRune == null)
            {
                actionController.BoardManager.GetBoardSquares(boardRow, boardColumn - 1).GetComponent<Highlight>().ApplyHighlighting(Color.yellow);
                actionController.BoardManager.AddGridSquareToHiglighted(actionController.BoardManager.GetBoardSquares(boardRow, boardColumn - 1));
            }
        }
    }
}
