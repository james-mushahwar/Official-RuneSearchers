using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    [SerializeField]
    private GameSettings gameSettings;

    [SerializeField]
    private TurnState turnState;

    [Header("Player references")]
    [SerializeField]
    private PlayerManager playerManager;
    [SerializeField]
    private Player currentPlayer = null;

    [Header("Game Events")]
    [SerializeField]
    private GameEvent StartBattle;
    [SerializeField]
    private GameEvent ChangeTurn;
    [SerializeField]
    private GameEvent EndBattle;

    public TurnState TurnState => turnState;

    public Player CurrentPlayer { get => currentPlayer; }

    public void Start()
    {
        turnState.BattleState = BattleStates.LoadBattle;
        StartBattle.Raise();
        StartCoroutine(DelayStart());
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(gameSettings.GameStartDelay);
        turnState.BattleState = BattleStates.Player1Turn;
        currentPlayer = playerManager.Player1;
    }

    public void ChangeTurns()
    {
        if (turnState.BattleState != BattleStates.Player1Turn &&
            turnState.BattleState != BattleStates.Player2Turn)
            return;
        //TO-Do Run current player end turn steps
        EndPlayerTurn();

        if (turnState.BattleState == BattleStates.Player1Turn)
        {
            turnState.BattleState = BattleStates.Player2Turn;
            currentPlayer = playerManager.Player2;
        }
        else if (turnState.BattleState == BattleStates.Player2Turn)
        {
            turnState.BattleState = BattleStates.Player1Turn;
            currentPlayer = playerManager.Player1;
        }

        // to-do run new current player start turn steps
        StartPlayerTurn();
        ChangeTurn.Raise();
    }

    private void EndPlayerTurn()
    {
        currentPlayer.PlayerGUI.HidePlayerGUI();
        currentPlayer.PlayerSpawns.ResetClassAbilities();
    }

    private void StartPlayerTurn()
    {
        currentPlayer.PlayerGUI.ShowPlayerGUI();
    }

    public void End()
    {
        turnState.BattleState = BattleStates.EndBattle;
        currentPlayer = null;
        EndBattle.Raise();
    }
}
