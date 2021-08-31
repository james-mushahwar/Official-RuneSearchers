using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Player", menuName = "Player/Player")]
public class Player : ScriptableObject
{
    [Header("Spawns and Cards")]
    [SerializeField]
    private PlayerSpawns playerSpawns;
    [SerializeField]
    private PlayerCards playerCards;

    [Header("In-play stats")]
    [SerializeField]
    private PlayerStats playerStats;

    [Header("Player preferences")]
    [SerializeField]
    private PlayerSettings playerSettings;

    [Header("Player GUI")]
    [SerializeField]
    private PlayerGUI playerGUI;

    [Header("External objects")]
    [SerializeField]
    private SpawnHandler spawnHandler;

    public PlayerSpawns PlayerSpawns => playerSpawns;
    public PlayerStats PlayerStats => playerStats;
    public SpawnHandler SpawnHandler => spawnHandler;
    public PlayerGUI PlayerGUI => playerGUI;
}
