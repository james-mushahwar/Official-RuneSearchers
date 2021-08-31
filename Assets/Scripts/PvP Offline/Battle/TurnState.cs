using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurnState : ScriptableObject
{
    [SerializeField]
    private BattleStates battleState;

    public BattleStates BattleState
    {
        get => battleState;
        set => battleState = value;
    }
}
