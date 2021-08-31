using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Player Load", menuName = "Load/Player")]
public class LoadPlayer : ScriptableObject
{
    [SerializeField]
    private GameObject unitPlaceholder;

    [Header("Player setup")]
    [SerializeField]
    private Player player;
    [SerializeField]
    private Unit[] playerSearcherChoices = new Unit[3];
    [SerializeField]
    private Unit[] playerRedChoices = new Unit[3];
    [SerializeField]
    private Unit[] playerYellowChoices = new Unit[3];
    [SerializeField]
    private Unit[] playerBlueChoices = new Unit[3];

    [Header("Player Unit tray setup")]
    [SerializeField]
    private GameObject playerSpawnTray;

    public void Setup(GameObject spawnTray)
    {
        playerSpawnTray = spawnTray;
        player.PlayerGUI.SpawnTray = spawnTray;

        GenerateUnits();
        SetUnitOccupant();
        SpawnTrayUnits();
    }

    private void GenerateUnits()
    {
        string playerName = player.name;

        for (int i = 0; i < 3; i++)
        {
            //searchers
            GameObject newSearcher = Instantiate(unitPlaceholder);
            newSearcher.name = playerName + "Searcher Lvl " + i;
            newSearcher.SetActive(false);
            newSearcher.GetComponent<InplayUnit>().Unit = playerSearcherChoices[i];
            newSearcher.GetComponent<SpriteRenderer>().sprite = newSearcher.GetComponent<InplayUnit>().Unit.UnitAppearance.UnitSprite;
            player.PlayerSpawns.SearcherUnitObjects.SetUnitToArray(i, newSearcher.GetComponent<InplayUnit>());

            //red
            GameObject newRed = Instantiate(unitPlaceholder);
            newRed.name = playerName + "Red Lvl " + i;
            newRed.SetActive(false);
            newRed.GetComponent<InplayUnit>().Unit = playerRedChoices[i];
            newRed.GetComponent<SpriteRenderer>().sprite = newRed.GetComponent<InplayUnit>().Unit.UnitAppearance.UnitSprite;
            player.PlayerSpawns.RedUnitObjects.SetUnitToArray(i, newRed.GetComponent<InplayUnit>());

            //yellow
            GameObject newYellow = Instantiate(unitPlaceholder);
            newYellow.name = playerName + "Yellow Lvl " + i;
            newYellow.SetActive(false);
            newYellow.GetComponent<InplayUnit>().Unit = playerYellowChoices[i];
            newYellow.GetComponent<SpriteRenderer>().sprite = newYellow.GetComponent<InplayUnit>().Unit.UnitAppearance.UnitSprite;
            player.PlayerSpawns.YellowUnitObjects.SetUnitToArray(i, newYellow.GetComponent<InplayUnit>());

            //blue
            GameObject newBlue = Instantiate(unitPlaceholder);
            newBlue.name = playerName + "Blue Lvl " + i;
            newBlue.SetActive(false);
            newBlue.GetComponent<InplayUnit>().Unit = playerBlueChoices[i];
            newBlue.GetComponent<SpriteRenderer>().sprite = newBlue.GetComponent<InplayUnit>().Unit.UnitAppearance.UnitSprite;
            player.PlayerSpawns.BlueUnitObjects.SetUnitToArray(i, newBlue.GetComponent<InplayUnit>());
        }
    }

    private void SetUnitOccupant()
    {
        PlayerSpawns playerSpawns = player.PlayerSpawns;
        Occupant playerOccupant = player.PlayerStats.PlayerOccupant;

        for (int i = 0; i < 3; i++)
        {
            playerSpawns.SearcherUnitObjects.Units[i].UnitState.UnitOccupant = playerOccupant;
            playerSpawns.RedUnitObjects.Units[i].UnitState.UnitOccupant = playerOccupant;
            playerSpawns.YellowUnitObjects.Units[i].UnitState.UnitOccupant = playerOccupant;
            playerSpawns.BlueUnitObjects.Units[i].UnitState.UnitOccupant = playerOccupant;
        }
    }

    private void SpawnTrayUnits()
    {
        Unit[][] unitsList = new Unit[3][];
        unitsList[0] = playerRedChoices;
        unitsList[1] = playerYellowChoices;
        unitsList[2] = playerBlueChoices;

        for (int i = 0; i < 3; i++)
        {
            playerSpawnTray.transform.GetChild(i * 3).gameObject.GetComponent<TrayUnitData>().Unit = unitsList[i][0];
            playerSpawnTray.transform.GetChild(i * 3).gameObject.GetComponent<Image>().sprite = playerSpawnTray.transform.GetChild(i * 3).gameObject.GetComponent<TrayUnitData>().Unit.UnitAppearance.UnitSprite;

            playerSpawnTray.transform.GetChild((i * 3) + 1).gameObject.GetComponent<TrayUnitData>().Unit = unitsList[i][1];
            playerSpawnTray.transform.GetChild((i * 3) + 1).gameObject.GetComponent<Image>().sprite = playerSpawnTray.transform.GetChild((i * 3) + 1).gameObject.GetComponent<TrayUnitData>().Unit.UnitAppearance.UnitSprite;

            playerSpawnTray.transform.GetChild((i * 3) + 2).gameObject.GetComponent<TrayUnitData>().Unit = unitsList[i][2];
            playerSpawnTray.transform.GetChild((i * 3) + 2).gameObject.GetComponent<Image>().sprite = playerSpawnTray.transform.GetChild((i * 3) + 2).gameObject.GetComponent<TrayUnitData>().Unit.UnitAppearance.UnitSprite;
        }
    }

    public GameObject GetSearcher(int searcherIndex)
    {
        return player.PlayerSpawns.SearcherUnitObjects.GetUnitByIndex(searcherIndex);
    }
}
