using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Manager", menuName = "Player/Player Manager")]
public class PlayerManager : ScriptableObject
{
    [Header("Player classes")]
    [SerializeField]
    private LoadPlayer loadPlayer1;
    [SerializeField]
    private LoadPlayer loadPlayer2;

    [SerializeField]
    private Player player1;
    [SerializeField]
    private Player player2;

    public LoadPlayer LoadPlayer1 => loadPlayer1;
    public LoadPlayer LoadPlayer2 => loadPlayer2;

    public Player Player1 { get => player1; }
    public Player Player2 { get => player2; }

    public Player GetPlayerOccupant(Occupant occ)
    {
        return occ == Occupant.Player1 ? player1 : player2;
    }
}
