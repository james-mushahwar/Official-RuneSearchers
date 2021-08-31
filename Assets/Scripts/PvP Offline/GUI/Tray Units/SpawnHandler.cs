using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Spawn Handler", menuName = "Spawn/Spawn Handler")]
public class SpawnHandler : ScriptableObject
{
    [Header("Spawn Handler current stats")]
    [SerializeField]
    private bool inSpawnMode = false;

    [SerializeField]
    private Spawner spawner;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Unit currentSelectedUnitSpawn;

    [Header("Spawn tray enable event")]
    [SerializeField]
    private GameEvent Level1RedIsEnabled;
    [SerializeField]
    private GameEvent Level2RedIsEnabled;
    [SerializeField]
    private GameEvent Level3RedIsEnabled;
    [SerializeField]
    private GameEvent Level1YellowIsEnabled;
    [SerializeField]
    private GameEvent Level2YellowIsEnabled;
    [SerializeField]
    private GameEvent Level3YellowIsEnabled;
    [SerializeField]
    private GameEvent Level1BlueIsEnabled;
    [SerializeField]
    private GameEvent Level2BlueIsEnabled;
    [SerializeField]
    private GameEvent Level3BlueIsEnabled;

    [Header("Spawn tray disable event")]
    [SerializeField]
    private GameEvent Level1RedIsDisabled;
    [SerializeField]
    private GameEvent Level2RedIsDisabled;
    [SerializeField]
    private GameEvent Level3RedIsDisabled;
    [SerializeField]
    private GameEvent Level1YellowIsDisabled;
    [SerializeField]
    private GameEvent Level2YellowIsDisabled;
    [SerializeField]
    private GameEvent Level3YellowIsDisabled;
    [SerializeField]
    private GameEvent Level1BlueIsDisabled;
    [SerializeField]
    private GameEvent Level2BlueIsDisabled;
    [SerializeField]
    private GameEvent Level3BlueIsDisabled;

    [Header ("External game events")]
    [SerializeField]
    private GameEvent UnitSelectedToSpawn;

    public Unit CurrentSelectedUnitSpawn
    {
        get => currentSelectedUnitSpawn;
        set => currentSelectedUnitSpawn = value;
    }
    public Spawner Spawner => spawner;

    private void OnEnable()
    {
        inSpawnMode = false;
        currentSelectedUnitSpawn = null;
    }

    public void SelectUnitToSpawn(Unit newUnitSelected)
    {
        Debug.Log("Unit selected to spawn");
        currentSelectedUnitSpawn = newUnitSelected;
        inSpawnMode = true;
        UnitSelectedToSpawn.Raise();
    }

    public bool IsInSpawnMode()
    {
        return inSpawnMode == true;
    }

    public void UpdateSpawnTrayUnits()
    {
        Debug.Log("Updating spawn tray units");
        UpdateSpawnClassUnits(player.PlayerStats.RedRunes);
        UpdateSpawnClassUnits(player.PlayerStats.YellowRunes);
        UpdateSpawnClassUnits(player.PlayerStats.BlueRunes);
    }

    private void UpdateSpawnClassUnits(RuneClassContainer runesContainer)
    {
        if (runesContainer.HasNoRunes())
        {
            DisableAllSpawnsInClass(runesContainer);
            return;
        }

        Debug.Log("Has runes");

        if (runesContainer.HasEqualOrMoreActiveRunes(1))
        {
            if (CanUnitBeSpawned(runesContainer, 1))
            {
                Debug.Log("Enable level 1 unit tray");
                EnableLevel1SpawnInClass(runesContainer);
            }
            else
            {
                DisableLevel1SpawnInClass(runesContainer);
            }
            if (runesContainer.HasEqualOrMoreActiveRunes(2))
            {
                if (CanUnitBeSpawned(runesContainer, 2))
                {
                    EnableLevel2SpawnInClass(runesContainer);
                }
                else
                {
                    DisableLevel2SpawnInClass(runesContainer);
                }
                if (runesContainer.HasEqualOrMoreActiveRunes(3))
                {
                    if (CanUnitBeSpawned(runesContainer, 3))
                    {
                        EnableLevel3SpawnInClass(runesContainer);
                    }
                    else
                    {
                        DisableLevel3SpawnInClass(runesContainer);
                    }
                }
                else
                {
                    DisableLevel3SpawnInClass(runesContainer);
                }
            }
            else
            {
                DisableLevel2SpawnInClass(runesContainer);
                DisableLevel3SpawnInClass(runesContainer);
            }
        }
        else
        {
            DisableAllSpawnsInClass(runesContainer);
        }
    }

    public void SetInSpawnMode(bool cond)
    {
        inSpawnMode = cond;
    }

    private bool CanUnitBeSpawned(RuneClassContainer runesContainer, int level)
    {
        if (runesContainer == player.PlayerStats.RedRunes)
        {
            switch (level)
            {
                case 1:
                    Debug.Log("Red level 1 is spawned = " + player.PlayerSpawns.RedUnitObjects.GetLevelOneUnit().UnitState.IsSpawned);
                    //Debug.Log("Unit coords are = " + player.PlayerSpawns.RedUnitObjects.GetLevelOneUnit().UnitState.BoardRow);
                    return !player.PlayerSpawns.RedUnitObjects.GetLevelOneUnit().UnitState.IsSpawned;
                case 2:
                    return !player.PlayerSpawns.RedUnitObjects.GetLevelTwoUnit().UnitState.IsSpawned;
                default:
                    return !player.PlayerSpawns.RedUnitObjects.GetLevelThreeUnit().UnitState.IsSpawned;
            }
        }
        else if (runesContainer == player.PlayerStats.YellowRunes)
        {
            switch (level)
            {
                case 1:
                    Debug.Log("Yellow level 1 is spawned = " + player.PlayerSpawns.YellowUnitObjects.GetLevelOneUnit().UnitState.IsSpawned);
                    //Debug.Log("Unit coords are = " + player.PlayerSpawns.YellowUnitObjects.GetLevelOneUnit().UnitState.BoardRow);
                    return !player.PlayerSpawns.YellowUnitObjects.GetLevelOneUnit().UnitState.IsSpawned;
                case 2:
                    return !player.PlayerSpawns.YellowUnitObjects.GetLevelTwoUnit().UnitState.IsSpawned;
                default:
                    return !player.PlayerSpawns.YellowUnitObjects.GetLevelThreeUnit().UnitState.IsSpawned;
            }
        }
        else
        {
            switch (level)
            {
                case 1:
                    Debug.Log("Blue level 1 is spawned = " + player.PlayerSpawns.BlueUnitObjects.GetLevelOneUnit().UnitState.IsSpawned);
                    //Debug.Log("Unit coords are = " + player.PlayerSpawns.BlueUnitObjects.GetLevelOneUnit().UnitState.BoardRow);
                    return !player.PlayerSpawns.BlueUnitObjects.GetLevelOneUnit().UnitState.IsSpawned;
                case 2:
                    return !player.PlayerSpawns.BlueUnitObjects.GetLevelTwoUnit().UnitState.IsSpawned;
                default:
                    return !player.PlayerSpawns.BlueUnitObjects.GetLevelThreeUnit().UnitState.IsSpawned;
            }
        }
    }

    private void EnableLevel1SpawnInClass(RuneClassContainer runesContainer)
    {
        if (runesContainer == player.PlayerStats.RedRunes)
        {
            Level1RedIsEnabled.Raise();
        }
        else if (runesContainer == player.PlayerStats.YellowRunes)
        {
            Level1YellowIsEnabled.Raise();
        }
        else
        {
            Level1BlueIsEnabled.Raise();
        }
    }
    private void EnableLevel2SpawnInClass(RuneClassContainer runesContainer)
    {
        if (runesContainer == player.PlayerStats.RedRunes)
        {
            Level2RedIsEnabled.Raise();
        }
        else if (runesContainer == player.PlayerStats.YellowRunes)
        {
            Level2YellowIsEnabled.Raise();
        }
        else
        {
            Level2BlueIsEnabled.Raise();
        }
    }
    private void EnableLevel3SpawnInClass(RuneClassContainer runesContainer)
    {
        if (runesContainer == player.PlayerStats.RedRunes)
        {
            Level3RedIsEnabled.Raise();
        }
        else if (runesContainer == player.PlayerStats.YellowRunes)
        {
            Level3YellowIsEnabled.Raise();
        }
        else
        {
            Level3BlueIsEnabled.Raise();
        }
    }

    private void DisableAllSpawnsInClass(RuneClassContainer runesContainer)
    {
        if (runesContainer == player.PlayerStats.RedRunes)
        {
            DisableAllRed();
        }
        else if (runesContainer == player.PlayerStats.YellowRunes)
        {
            DisableAllYellow();
        }
        else
        {
            DisableAllBlue();
        }
    }

    private void DisableLevel1SpawnInClass(RuneClassContainer runesContainer)
    {
        if (runesContainer == player.PlayerStats.RedRunes)
        {
            Level1RedIsDisabled.Raise();
        }
        else if (runesContainer == player.PlayerStats.YellowRunes)
        {
            Level1YellowIsDisabled.Raise();
        }
        else
        {
            Level1BlueIsDisabled.Raise();
        }
    }
    private void DisableLevel2SpawnInClass(RuneClassContainer runesContainer)
    {
        if (runesContainer == player.PlayerStats.RedRunes)
        {
            Level2RedIsDisabled.Raise();
        }
        else if (runesContainer == player.PlayerStats.YellowRunes)
        {
            Level2YellowIsDisabled.Raise();
        }
        else
        {
            Level2BlueIsDisabled.Raise();
        }
    }
    private void DisableLevel3SpawnInClass(RuneClassContainer runesContainer)
    {
        if (runesContainer == player.PlayerStats.RedRunes)
        {
            Level3RedIsDisabled.Raise();
        }
        else if (runesContainer == player.PlayerStats.YellowRunes)
        {
            Level3YellowIsDisabled.Raise();
        }
        else
        {
            Level3BlueIsDisabled.Raise();
        }
    }

    private void DisableAllRed()
    {
        Level1RedIsDisabled.Raise();
        Level2RedIsDisabled.Raise();
        Level3RedIsDisabled.Raise();
    }
    private void DisableAllYellow()
    {
        Level1YellowIsDisabled.Raise();
        Level2YellowIsDisabled.Raise();
        Level3YellowIsDisabled.Raise();
    }
    private void DisableAllBlue()
    {
        Level1BlueIsDisabled.Raise();
        Level2BlueIsDisabled.Raise();
        Level3BlueIsDisabled.Raise();
    }
}
