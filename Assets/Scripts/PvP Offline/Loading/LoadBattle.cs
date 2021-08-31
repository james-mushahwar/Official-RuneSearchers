using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBattle : MonoBehaviour
{
    [Header("Board Setup")]
    [SerializeField]
    public BoardSettings BoardSettings;
    private int BoardRows;
    private int BoardColumns;
    [SerializeField]
    private GameObject BoardSquare;
    [SerializeField]
    private GameObject Board;
    private BoardManager BoardManager;

    [Header("Player Setup")]
    [SerializeField]
    private PlayerManager playerManager;
    [Header("Player Unit tray setup")]
    [SerializeField]
    private GameObject player1SpawnTray;
    [SerializeField]
    private GameObject player2SpawnTray;

    [Header("Spawn handling")]
    [SerializeField]
    private Spawner spawner;

    [Header("Rune Spawning")]
    [SerializeField]
    private RuneManager runeManager;

    void Start()
    {
        BoardManager = Board.GetComponent<BoardManager>();
        BoardRows = BoardSettings.BoardRows;
        BoardColumns = BoardSettings.BoardColumns;

        BoardSetup();
        PlayerSetup();
        SearcherSetup();
        RuneSetup();
    }

    private void RuneSetup()
    {
        bool RuneCanBePlaced;
        int runeRowIndex;
        int runeColumnIndex;
        GameObject newRune;
        GameObject newBoardSquare;

        for (int i = 0; i < runeManager.RuneSettings.MaxRunesInPlay; i++)
        {
            newRune = runeManager.InstantiateRune();
            RuneCanBePlaced = false;

            while (!RuneCanBePlaced)
            {
                runeRowIndex = Random.Range(0, BoardRows);
                runeColumnIndex = Random.Range(0, (BoardColumns - 1));
                newBoardSquare = BoardManager.GetBoardSquares(runeRowIndex, runeColumnIndex);

                if (BoardManager.GetUnitOnBoardSquare(runeRowIndex, runeColumnIndex) == null &&
                    BoardManager.GetRuneOnBoardSquare(newBoardSquare) == null)
                {
                    RuneCanBePlaced = true;
                    newRune.transform.SetParent(newBoardSquare.transform, false);
                    newBoardSquare.GetComponent<BoardSquareContainer>().BoardRune = newRune;
                }
            }
        }
    }

    private void SearcherSetup()
    {
        int searcherRowIndex;
        int searcherColumnIndex;
        bool searcherPositionSuccess;

        for (int i = 0; i < 3; i++)
        {
            searcherPositionSuccess = false;

            while (searcherPositionSuccess == false)
            {
                searcherRowIndex = Random.Range(0, BoardRows);
                searcherColumnIndex = Random.Range(0, (BoardColumns - 1) / 2);

                if (BoardManager.GetUnitOnBoardSquare(searcherRowIndex, searcherColumnIndex) == null)
                {
                    searcherPositionSuccess = true;
                    GameObject searcherPlayerOne = playerManager.LoadPlayer1.GetSearcher(i);
                    GameObject boardSquarePlayerOne = BoardManager.GetBoardSquares(searcherRowIndex, searcherColumnIndex);
                    spawner.SpawnUnit(searcherPlayerOne, boardSquarePlayerOne, searcherRowIndex, searcherColumnIndex);

                    int mirroredRow = BoardRows - searcherRowIndex - 1;
                    int mirroredColumn = BoardColumns - searcherColumnIndex - 1;
                    GameObject searcherPlayerTwo = playerManager.LoadPlayer2.GetSearcher(i);
                    GameObject boardSquarePlayerTwo = BoardManager.GetBoardSquares(mirroredRow, mirroredColumn);
                    spawner.SpawnUnit(searcherPlayerTwo, boardSquarePlayerTwo, mirroredRow, mirroredColumn);
                }
            }
        }
    }

    private void BoardSetup()
    {
        SetUpBoardVariables();
        SpawnBoardSquares();
        BoardAppearanceSetup();
        SetUpBoardSquares();
    }

    private void SetUpBoardVariables()
    {
        BoardManager.BoardRows = BoardRows;
        BoardManager.BoardColumns = BoardColumns;
    }

    private void SpawnBoardSquares()
    {
        // rows
        for (int i = 0; i < BoardRows; i++)
        {
            GameObject boardRow = new GameObject("Board Row "+i);
            boardRow.transform.parent = Board.transform;
            //set position
            boardRow.transform.position = new Vector3(i, 0, 0);

            // columns
            for (int j = 0; j < BoardColumns; j++)
            {
                GameObject boardSquare = Instantiate(BoardSquare, boardRow.transform);
                boardSquare.transform.position = new Vector3(j, i, 0);
                boardSquare.GetComponent<BoardSquareContainer>().RowIndex = i;
                boardSquare.GetComponent<BoardSquareContainer>().ColumnIndex = j;
            }
        }
    }

    private void BoardAppearanceSetup()
    {
        //position main camera over centre of the board
        float midRow = Mathf.Ceil(BoardRows / 2);
        float midColumn = Mathf.Ceil(BoardColumns / 2);

        Board.gameObject.transform.Rotate(BoardSettings.BoardXRotation, 0f, 0f);
        Camera.main.transform.position = Board.transform.position + new Vector3(midColumn, 0, 0) + BoardSettings.CameraPosition;
    }

    private void SetUpBoardSquares()
    {
        BoardManager.BoardSquares = new GameObject[BoardRows][]; 

        foreach (Transform row in Board.transform)
        {
            int rowIndex = row.GetSiblingIndex();
            BoardManager.BoardSquares[rowIndex] = new GameObject[BoardColumns];

            foreach (Transform col in row)
            {
                int columnIndex = col.GetSiblingIndex();
                BoardManager.BoardSquares[rowIndex][columnIndex] = col.gameObject;
            }
        }
    }

    private void PlayerSetup()
    {
        playerManager.LoadPlayer1.Setup(player1SpawnTray);
        playerManager.LoadPlayer2.Setup(player2SpawnTray);
    }
}
