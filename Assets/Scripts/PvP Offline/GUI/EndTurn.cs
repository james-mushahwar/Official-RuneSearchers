using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    [SerializeField]
    private TurnController turnController;

    public void OnClick()
    {
        turnController.ChangeTurns();
    }
}
