using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    [SerializeField]
    private float gameStartDelay;

    public float GameStartDelay
    {
        get
        {
            return gameStartDelay;
        }
    }
}
